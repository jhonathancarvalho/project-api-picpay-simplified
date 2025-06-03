using PicPaySimplified.Models.Entities;
using PicPaySimplified.Models.Enum;

namespace PicPaySimplified.Services.Validator
{
    public class TransferValidator : ITransferValidator
    {
        public string? Validate(WalletEntity sender, WalletEntity receiver, decimal amount)
        {
            if (sender is null)
                return "Carteira do remetente não encontrada.";

            if (receiver is null)
                return "Carteira do destinatário não encontrada.";

            if (sender.UserType == UserType.Shopkeeper)
                return "Lojistas não estão autorizados a realizar transferências.";

            if (amount <= 0)
                return "O valor da transferência deve ser maior que zero.";

            if (sender.AccountBalance < amount) 
                return "Saldo insuficiente.";

            return null;
        }
    }
}
