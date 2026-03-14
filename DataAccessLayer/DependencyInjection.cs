using ECommerce.DataAccessLayer.RepositoryContracts;
using ECommerce.DataAccessLayer.Context;
using ECommerce.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.ProductsService.DataAccessLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configration)
    {
        //Register your data access layer services into the IoC container here.
        string connectionStringTemplate = configration.GetConnectionString("DefaultConnection")!;
        string connectionString = connectionStringTemplate.Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
                                                            .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
                                                            .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
                                                            .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"))
                                                            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

        services.AddDbContext<ApplicationDbContext>
           (options =>
           {
               options.UseMySQL(connectionString);
           });

        services.AddScoped<IProductsRepository,ProductReprository>();
        return services;
    }
}
