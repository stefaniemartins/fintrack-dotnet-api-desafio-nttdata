using FinTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Conta> Contas => Set<Conta>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Conta)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.ContaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Categoria)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}