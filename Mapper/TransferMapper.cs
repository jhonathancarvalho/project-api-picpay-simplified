using PicPaySimplified.Models.DTO;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Mapper
{
    public static class TransferMapper
    {
        public static TransferDto ToTransferDTO(this TransferEntity transfer)
        {
            return new TransferDto(
                transfer.IdTransfer,
                transfer.Sender,
                transfer.Receiver,
                transfer.Amount
            );
        }
    }
}