using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOJ.ProductManagement.Infrastructure.Extensions;
using System.Reflection;

namespace MOJ.ProductManagement.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureLayer(configuration);

            services.AddAutoMapper();
            services.AddMediator();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
