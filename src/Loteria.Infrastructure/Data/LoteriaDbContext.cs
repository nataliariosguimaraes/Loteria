using Loteria.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Infrastructure.Data;

public sealed class LoteriaDbContext : DbContext
{
    public LoteriaDbContext(DbContextOptions<LoteriaDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApostaEntity> Apostas => Set<ApostaEntity>();

    public DbSet<ApostaDezenaEntity> ApostaDezenas => Set<ApostaDezenaEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApostaEntity>(builder =>
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.DataCriacao).IsRequired();
            builder.HasMany(a => a.Dezenas)
                .WithOne(d => d.Aposta)
                .HasForeignKey(d => d.ApostaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApostaDezenaEntity>(builder =>
        {
            builder.HasKey(d => new { d.ApostaId, d.Dezena });
            builder.Property(d => d.Dezena).IsRequired();
        });
    }
}
