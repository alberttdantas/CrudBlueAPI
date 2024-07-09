
using CrudBlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudBlue.Infrastructure.Data;

public class AgendaDbContext : DbContext
{
    public AgendaDbContext() { }

    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Active)
                .IsRequired();
        });
    }
}
