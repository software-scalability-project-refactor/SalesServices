using Microsoft.EntityFrameworkCore;
using SalesService.Entities;

namespace SalesService;

public class SalesDbContext : DbContext
{
    public DbSet<Sale> Sales { get; set; }

    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones adicionales del modelo
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.ProductIds).HasColumnName("Products");
            // Otras configuraciones si son necesarias
        });
    }
}