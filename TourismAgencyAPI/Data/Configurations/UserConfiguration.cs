using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasColumnType("CHAR(36)")
            .IsRequired();

        builder.Property(user => user.Username)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(320);
        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(97);

        builder.Property(user => user.Role)
            .HasConversion<string>()
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
    }
}