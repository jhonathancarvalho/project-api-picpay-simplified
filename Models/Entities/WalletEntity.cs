using PicPaySimplified.Models.Enum;
using PicPaySimplified.Models.Enums;

namespace PicPaySimplified.Models.Entities
{
    public class WalletEntity
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public DocumentType DocumentType { get; private set; }
        public string DocumentNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } 
        public decimal AccountBalance { get; private set; }
        public UserType UserType { get; set; }

        public WalletEntity() { }
        public WalletEntity(
            string fullName,
            DocumentType documentType,
            string documentNumber,
            string email,
            string password,
            decimal accountBalance = 0)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Password = password;
            AccountBalance = accountBalance;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor a ser debitado deve ser maior que zero.");

            if (amount > AccountBalance)
                throw new InvalidOperationException("Saldo insuficiente para realizar o débito.");

            AccountBalance -= amount;
        }

        public void Credit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor a ser creditado deve ser maior que zero.");

            AccountBalance += amount;
        }
    }
}
