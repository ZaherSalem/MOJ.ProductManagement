using Microsoft.EntityFrameworkCore;

namespace MOJ.ProductManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //DbSet

        //Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //ApplyConfiguration
        }
    }
}