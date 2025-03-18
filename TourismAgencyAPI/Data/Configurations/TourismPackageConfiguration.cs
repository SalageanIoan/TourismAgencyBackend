using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Data.Configurations;

public class TourismPackageConfiguration : IEntityTypeConfiguration<TourismPackage>
{
    public void Configure(EntityTypeBuilder<TourismPackage> builder)
    {
        builder.ToTable("TourismPackages");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.ImageData)
            .IsRequired()
            .HasColumnType("longblob"); 
    }
}