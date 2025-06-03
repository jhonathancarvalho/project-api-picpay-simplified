using Microsoft.EntityFrameworkCore.Storage;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Repositories.Transfer
{
    public interface ITransferRepository
    {
        Task AddTransactionAsync(TransferEntity entity);

        Task CommitAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}