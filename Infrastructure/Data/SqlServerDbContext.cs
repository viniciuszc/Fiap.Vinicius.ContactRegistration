using Domain.Entity;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class SqlServerDbContext : DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<DDD> DDDs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new DDDConfiguration());

        modelBuilder.Entity<Contact>()
            .HasOne(e => e.DDD)
            .WithMany(e => e.Contacts)
            .HasForeignKey(e => e.DDDId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
