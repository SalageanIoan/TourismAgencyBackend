using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Timestamp)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Package)
                .WithMany()
                .HasForeignKey(t => t.PackageId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.ToTable("Transactions");
        }
    }
}