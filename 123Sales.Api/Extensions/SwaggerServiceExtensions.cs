using Microsoft.OpenApi.Models;

namespace _123Sales.Api.Extensions
{
    /// <summary>
    /// Extension methods for configuring Swagger services.
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Adds Swagger services to the DI container.
        /// </summary>
        /// <param name="services">The IServiceCollection to which Swagger services will be added.</param>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "123Sales API", Version = "v1" });
            });

            return services;
        }

        /// <summary>
        /// Configures the application to use Swagger and SwaggerUI.
        /// </summary>
        /// <param name="app">
        /// The IApplicationBuilder instance for configuring the application pipeline.
        /// </param>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "123Sales API v1");
            });

            return app;
        }
    }
}