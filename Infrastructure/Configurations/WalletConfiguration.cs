using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<WalletEntity>
    {
        public void Configure(EntityTypeBuilder<WalletEntity> builder)
        {
            builder.HasIndex(e => new { e.DocumentNumber, e.Email }).IsUnique();

            builder.Property(e => e.AccountBalance)
                   .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.DocumentType)
                   .HasConversion<string>();

            builder.Property(e => e.UserType)
                   .HasConversion<string>();
        }
    }
}