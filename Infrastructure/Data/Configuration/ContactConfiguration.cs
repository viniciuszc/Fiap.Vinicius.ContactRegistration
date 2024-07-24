using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("CONTATO");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("CODIGO");

        builder.Property(x => x.Name)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasColumnName("TELEFONE")
            .HasMaxLength(20)
            .IsRequired();
    }
}