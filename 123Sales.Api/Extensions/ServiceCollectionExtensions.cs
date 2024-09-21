using _123Sales.Domain.Interfaces.Repositories;
using _123Sales.Domain.Interfaces.Services;
using _123Sales.Infra.Context;
using _123Sales.Infra.Repositories;
using _123Sales.Services;
using Microsoft.EntityFrameworkCore;

namespace _123Sales.Api.Extensions
{
    /// <summary>
    /// Extension methods for configuring services and repositories.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the application's services and repositories to the DI container.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services will be added.</param>
        /// <param name="configuration">The application's configuration object.</param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext com SQL Server
            services.AddDbContext<SalesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registrar repositórios e serviços
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleService, SaleService>();

            return services;
        }
    }
}