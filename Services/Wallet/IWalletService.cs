using PicPaySimplified.Models.Entities;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Models.Result;

namespace PicPaySimplified.Services.Wallet
{
    public interface IWalletService
    {
        Task<Result<bool>> ExecuteAsync(WalletRequest request);
    }
}
