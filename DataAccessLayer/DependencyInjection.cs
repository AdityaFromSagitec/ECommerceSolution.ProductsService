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
       services.AddDbContext<ApplicationDbContext>
           (options =>
           {
               options.UseMySQL(configration.GetConnectionString("DefaultConnection")!);//GetConnectionString() is nullable to handle this ! is added
           });

    //    services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseMySql(
    //    configration.GetConnectionString("DefaultConnection")!,
    //    ServerVersion.AutoDetect(configration.GetConnectionString("DefaultConnection")!)
    //)
//);
        services.AddScoped<IProductsRepository,ProductReprository>();
        return services;
    }
}
