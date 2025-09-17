
using ECommerce.BussinessLogicLayer.Mappers;
using ECommerce.BussinessLogicLayer.ServiceContracts;
using ECommerce.BussinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.ProductsService.BussinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBussinessLogicLayer(this IServiceCollection services)
        {
            // Register your bussiness access layer services into the IoC container here.
           // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); OR
           services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
            //mentioin the assembly where the validators are located makes it work for all validators in that assembly
            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

            services.AddScoped<IProductService, ECommerce.BussinessLogicLayer.Services.ProductService>();
            return services;
        }
    }
}
