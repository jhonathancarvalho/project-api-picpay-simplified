namespace PicPaySimplified.Models.Entities
{
    public class TransferEntity
    {
        public Guid IdTransfer { get; private set; }
        public Guid SenderId { get; private set; }
        public WalletEntity Sender { get; private set; }
        public Guid ReceiverId { get; private set; }
        public WalletEntity Receiver { get; private set; }
        public decimal Amount { get; private set; }

        public TransferEntity() { }

        public TransferEntity(Guid senderId, WalletEntity sender, Guid receiverId, WalletEntity receiver, decimal amount)
        {
            IdTransfer = Guid.NewGuid();
            SenderId = senderId;
            Sender = sender;
            ReceiverId = receiverId;
            Receiver = receiver;
            Amount = amount;
        }
    }
}