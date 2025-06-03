using PicPaySimplified.Models.DTO;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Models.Result;

namespace PicPaySimplified.Services.Transfer
{
    public interface ITransferService
    {
        Task<Result<TransferDto>> ExecuteAsync(TransferRequest request);
    }
}
