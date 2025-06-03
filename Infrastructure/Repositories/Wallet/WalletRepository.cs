using Microsoft.EntityFrameworkCore;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Repositories.Wallet
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WalletEntity wallet)
        {
            await _context.Wallets.AddAsync(wallet);
        }

        public Task UpdateAsync(WalletEntity wallet)
        {
            _context.Wallets.Update(wallet);
            return Task.CompletedTask;
        }

        public async Task<WalletEntity?> GetByDocumentAsync(string documentNumber, string email)
        {
            return await _context.Wallets.FirstOrDefaultAsync(wallet =>
                wallet.DocumentNumber == documentNumber || wallet.Email == email);
        }

        public async Task<WalletEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Wallets
                .AsNoTracking() 
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
