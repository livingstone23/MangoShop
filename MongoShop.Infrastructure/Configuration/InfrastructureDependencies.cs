


using MangoShop.Domain.interfaces;
using MangoShop.Domain.services;
using MangoShop.Infraestructure.Context;
using MangoShop.Infraestructure.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace MangoShop.Infraestructure.configuration;



/// <summary>
/// Static class that contains all the dependencies of the infrastructure dependencies in the IServiceCollection.
/// Enables dependency injection of serices and repositories. 
/// </summary>
public static class InfrastructureDependencies
{
    
    
    public static IServiceCollection RegisterInfrastureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        
        // Register the MangoDbContext with SQL Server connection string from configuration
        services.AddDbContext<MangoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });


        // Register the MangoDbContext for dependency injection
        services.AddScoped<MangoDbContext>();



        services.AddScoped<IWhatsAppMessageRepository, WhatsAppMessageRepository>();

        // Register all services for dependency injection
        services.AddScoped<IWhatsAppMessageService, WhatsAppMessageService>();


        return services;

    }

}
