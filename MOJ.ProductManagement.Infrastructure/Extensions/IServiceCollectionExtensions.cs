using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOJ.ProductManagement.Domain.Interfaces;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Infrastructure.Data;
using MOJ.ProductManagement.Infrastructure.Data.Seed;
using MOJ.ProductManagement.Infrastructure.Repositories;
using System.Reflection;

namespace MOJ.ProductManagement.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
            services.AddDatabaseInitializer();
            services.addUnitOfWork();
            services.addDbFactory();
            services.addURepositories();
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
        private static void addUnitOfWork(this IServiceCollection services)
        {
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        private static void addDbFactory(this IServiceCollection services)
        {
            // Register DbFactory
            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>());

            services.AddScoped<DbFactory<ApplicationDbContext>>();
        }
        private static void addURepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
                       .FromAssemblies(Assembly.GetExecutingAssembly())
                       .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                       .AsImplementedInterfaces()
                       .WithTransientLifetime());
        }
    }
}
