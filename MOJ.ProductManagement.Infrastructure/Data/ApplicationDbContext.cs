using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Infrastructure.Data.Configurations;

namespace MOJ.ProductManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // Lookups
        public DbSet<Lookup> Lookups { get; set; }


        // Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new LookupConfiguration());
        }
    }
}