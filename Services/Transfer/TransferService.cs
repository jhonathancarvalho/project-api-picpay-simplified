using Microsoft.EntityFrameworkCore.Storage;
using PicPaySimplified.Infrastructure.Repositories.Transfer;
using PicPaySimplified.Infrastructure.Repositories.Wallet;
using PicPaySimplified.Mapper;
using PicPaySimplified.Models.DTO;
using PicPaySimplified.Models.Entities;
using PicPaySimplified.Models.Enum;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Models.Result;
using PicPaySimplified.Services.Authorizer;
using PicPaySimplified.Services.Notification;
using PicPaySimplified.Services.Validator;

namespace PicPaySimplified.Services.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IAuthorizerService _authorizerService;
        private readonly INotificationService _notificationService;

        public TransferService(ITransferRepository transferRepository, IWalletRepository walletRepository,
            IAuthorizerService authorizerService, INotificationService notificationService)
        {
            _transferRepository = transferRepository;
            _walletRepository = walletRepository;
            _authorizerService = authorizerService;
            _notificationService = notificationService;
        }

        public async Task<Result<TransferDto>> ExecuteAsync(TransferRequest request)
        {
            if (!await _authorizerService.AuthorizeAsync())
                return Result<TransferDto>.Failure("Não autorizado");

            var sender = await _walletRepository.GetByIdAsync(request.SenderId);
            var receiver = await _walletRepository.GetByIdAsync(request.ReceiverId);

            Console.WriteLine($"Sender Id: {sender?.Id}, UserType: {sender?.UserType}");

            if (sender is null || receiver is null)
                return Result<TransferDto>.Failure("Carteira não encontrada");

            if (sender.AccountBalance < request.Amount || sender.AccountBalance == 0)
                return Result<TransferDto>.Failure("Saldo insuficiente");

            if (sender.UserType == UserType.Shopkeeper)
                return Result<TransferDto>.Failure("Lojista não pode efetuar transferências");

            sender.Debit(request.Amount);
            receiver.Credit(request.Amount);

            var transfer = new TransferEntity(
                                                sender.Id,
                                                sender,
                                                receiver.Id,
                                                receiver,
                                                request.Amount
            );

            using (var transactionScope = await _transferRepository.BeginTransactionAsync())
            {
                try
                {
                    await ExecuteParallelOperationsAsync(sender, receiver, transfer);
                    await CommitTransactionAsync(transactionScope);

                    var transferId = transfer.IdTransfer;

                    Console.WriteLine($"Transferência registrada com ID: {transferId}");
                }
                catch (Exception ex)
                {
                    await RollbackTransactionAsync(transactionScope);
                    return Result<TransferDto>.Failure("Erro ao realizar a transferência: " + ex.Message);
                }
            }

            await _notificationService.SendNotificationAsync();
            return Result<TransferDto>.Success(transfer.ToTransferDTO());
        }

        private async Task ExecuteParallelOperationsAsync(WalletEntity sender, WalletEntity receiver,
            TransferEntity transfer)
        {
            var tasks = new List<Task>
        {
            _walletRepository.UpdateAsync(sender),
            _walletRepository.UpdateAsync(receiver),
            _transferRepository.AddTransactionAsync(transfer)
        };

            await Task.WhenAll(tasks);
        }

        private async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            await _transferRepository.CommitAsync();
            await transaction.CommitAsync();
        }

        private async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }
    }
}