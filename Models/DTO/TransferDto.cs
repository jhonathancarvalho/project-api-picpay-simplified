using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Models.DTO
{
    public record TransferDto(
        Guid TransferId,
        WalletEntity Sender,
        WalletEntity Receiver,
        decimal TransferredAmount
    );
}