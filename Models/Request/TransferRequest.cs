using System.ComponentModel.DataAnnotations;

namespace PicPaySimplified.Models.Request
{
    public class TransferRequest
    {
        [Required(ErrorMessage = "O valor da transferência é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "O identificador do remetente (SenderId) é obrigatório.")]
        public Guid SenderId { get; set; }

        [Required(ErrorMessage = "O identificador do destinatário (ReceiverId) é obrigatório.")]
        public Guid ReceiverId { get; set; }
    }
}