using PicPaySimplified.Models.Enum;
using PicPaySimplified.Models.Enums;
using PicPaySimplified.Ultils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PicPaySimplified.Models.Request
{
    public class WalletRequest
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O tipo de documento é obrigatório.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "O número do documento é obrigatório.")]
        [DocumentNumberValidation(ErrorMessage = "O número do documento informado é inválido.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O saldo da conta é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O saldo da conta não pode ser negativo.")]
        public decimal AccountBalance { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
    }
}
