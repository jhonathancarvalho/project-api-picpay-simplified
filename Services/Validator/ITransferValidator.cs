using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Services.Validator
{
    public interface ITransferValidator
    {
        /// <summary>
        /// Valida os dados da transferência e retorna uma mensagem de erro caso haja alguma violação.
        /// Retorna null se a validação for bem-sucedida.
        /// </summary>
        /// <param name="sender">Carteira do remetente</param>
        /// <param name="receiver">Carteira do destinatário</param>
        /// <param name="amount">Valor da transferência</param>
        /// <returns>Mensagem de erro ou null se válido</returns>
        string? Validate(WalletEntity sender, WalletEntity receiver, decimal amount);
    }
}