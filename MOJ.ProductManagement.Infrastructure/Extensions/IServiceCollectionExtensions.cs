using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOJ.ProductManagement.Infrastructure.Data;
using MOJ.ProductManagement.Infrastructure.Data.Seed;

namespace MOJ.ProductManagement.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
            services.AddDatabaseInitializer();
        }
        private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        private static void AddDatabaseInitializer(this IServiceCollection services)
        {
            services.AddTransient<DatabaseInitializer>();

        }
    }
}
