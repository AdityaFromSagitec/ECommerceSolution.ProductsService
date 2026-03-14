

using AutoMapper;
using ECommerce.BussinessLogicLayer.DTO;
using ECommerce.DataAccessLayer.Entites;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ECommerce.BussinessLogicLayer.Mappers;
public class ProductAddRequestToProductMappingProfile: Profile
{
    public ProductAddRequestToProductMappingProfile()
    {
        //soucce is DTO.ProductAddRequest and destination is DataAccessLayer.Entites.Product
        CreateMap<ProductAddRequest, Product>()
            .ForMember(dest => dest.ProductID, opt => opt.Ignore())
            .ForMember(dest=> dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
