using Microsoft.EntityFrameworkCore;
using SeguroApi.Models;

public class SeguroContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Garantia> Garantias { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItemVendas { get; set; }

    public SeguroContext(DbContextOptions<SeguroContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        modelBuilder.Entity<Garantia>().HasKey(g => g.Id);
        modelBuilder.Entity<Venda>().HasKey(v => v.Id);
        modelBuilder.Entity<ItemVenda>().HasKey(iv => iv.Id);
    }
}