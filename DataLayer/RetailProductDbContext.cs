using Microsoft.EntityFrameworkCore;
using RetailProductService.Models;

namespace RetailProductService.Data
{
public class RetailProductDbContext : DbContext
{
    public RetailProductDbContext(DbContextOptions<RetailProductDbContext> options) :base(options){}
    public DbSet<ProductCatalog> ProductCatalogs {get;set;}

    public DbSet<ProductQueue> ProductQueues {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCatalog>().HasKey(p=>p.Id);
        modelBuilder.Entity<ProductQueue>().HasKey(a=>a.Id);
    }
}
}