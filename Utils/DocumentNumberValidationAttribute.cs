using PicPaySimplified.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PicPaySimplified.Ultils
{
    public class DocumentNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var documentNumber = value as string;

            if (string.IsNullOrWhiteSpace(documentNumber))
                return new ValidationResult(ErrorMessage ?? "O número do documento é obrigatório.");

            var instance = validationContext.ObjectInstance;
            var typeProperty = validationContext.ObjectType.GetProperty("DocumentType");

            if (typeProperty == null)
                return new ValidationResult("Não foi possível validar o tipo de documento.");

            var documentType = (DocumentType)typeProperty.GetValue(instance);

            bool isValid = documentType switch
            {
                DocumentType.CPF => DocumentNumberValidator.IsCpf(documentNumber),
                DocumentType.CNPJ => DocumentNumberValidator.IsCnpj(documentNumber),
                _ => false
            };

            if (!isValid)
                return new ValidationResult(ErrorMessage ?? "Número do documento inválido para o tipo informado.");

            return ValidationResult.Success;
        }
    }
}