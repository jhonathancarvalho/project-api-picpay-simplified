using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Repositories.Wallet
{
    public interface IWalletRepository
    {
        Task AddAsync(WalletEntity wallet);

        Task UpdateAsync(WalletEntity wallet);

        Task<WalletEntity?> GetByDocumentAsync(string documentNumber, string email);

        Task<WalletEntity?> GetByIdAsync(Guid id);

        Task CommitAsync();
    }
}
