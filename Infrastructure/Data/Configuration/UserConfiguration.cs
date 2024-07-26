using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USUARIO");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("CODIGO");

        builder.Property(x => x.Name)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("SENHA")
            .HasMaxLength(100)
            .IsRequired();
    }
}