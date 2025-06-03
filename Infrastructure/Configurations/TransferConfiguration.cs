using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PicPaySimplified.Models.Entities;

namespace PicPaySimplified.Infrastructure.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<TransferEntity>
    {
        public void Configure(EntityTypeBuilder<TransferEntity> builder)
        {
            builder.HasKey(e => e.IdTransfer);

            builder.HasOne(e => e.Sender)
                   .WithMany()
                   .HasForeignKey(e => e.SenderId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Transfer_Sender");

            builder.HasOne(e => e.Receiver)
                   .WithMany()
                   .HasForeignKey(e => e.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Transfer_Receiver");

            builder.Property(e => e.Amount)
                   .HasColumnType("decimal(18, 2)");
        }
    }
}