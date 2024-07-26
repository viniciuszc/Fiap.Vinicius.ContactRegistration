using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class DDDConfiguration : IEntityTypeConfiguration<DDD>
{
    public void Configure(EntityTypeBuilder<DDD> builder)
    {
        builder.ToTable("DDD");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ID");       

        builder.Property(x => x.Code)
            .HasColumnName("CODIGO")
            .HasMaxLength(3)
            .IsRequired(); ;

        builder.Property(x => x.Region)
            .HasColumnName("REGIAO")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.UF)
            .HasColumnName("UF")
            .HasMaxLength(2)
            .IsRequired();
    }
}