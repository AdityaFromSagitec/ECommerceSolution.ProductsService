using AutoMapper;
using ECommerce.BussinessLogicLayer.DTO;
using ECommerce.DataAccessLayer.Entites;

namespace ECommerce.BussinessLogicLayer.Mappers;
internal class ProductUpdateRequestToProductMappingProfile : Profile
{
    public ProductUpdateRequestToProductMappingProfile()
    {
        //soucce is DTO.ProductAddRequest and destination is DataAccessLayer.Entites.Product
        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
