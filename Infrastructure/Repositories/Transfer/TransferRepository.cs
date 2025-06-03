using Microsoft.EntityFrameworkCore.Storage;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Repositories.Transfer
{
    public class TransferRepository : ITransferRepository
    {
        private readonly ApplicationDbContext _context;

        public TransferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(TransferEntity entity)
        {
            await _context.Transfers.AddAsync(entity);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar alterações no banco de dados:");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw; // repropaga a exceção para manter o fluxo de erro
            }
        }
    }
}