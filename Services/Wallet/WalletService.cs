using PicPaySimplified.Infrastructure.Repositories.Wallet;
using PicPaySimplified.Models.Entities;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Models.Result;

namespace PicPaySimplified.Services.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _repository;

        public WalletService(IWalletRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> ExecuteAsync(WalletRequest request)
        {
            var walletExists = await _repository.GetByDocumentAsync(request.DocumentNumber, request.Email);

            if (walletExists is not null)
                return Result<bool>.Failure("A carteira já existe.");

            var wallet = new WalletEntity(
                request.FullName,
                request.DocumentType,
                request.DocumentNumber,
                request.Email,
                request.Password,
                request.AccountBalance
            )
            {
                UserType = request.UserType
            };

            await _repository.AddAsync(wallet);
            await _repository.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
