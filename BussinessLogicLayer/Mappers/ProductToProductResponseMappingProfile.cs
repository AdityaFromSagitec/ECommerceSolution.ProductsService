using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.BussinessLogicLayer.DTO;
using ECommerce.DataAccessLayer.Entites;

namespace BussinessLogicLayer.Mappers
{
    internal class ProductToProductResponseMappingProfile : Profile
    {
        public ProductToProductResponseMappingProfile()
        {
            //soucce is DTO.ProductAddRequest and destination is DataAccessLayer.Entites.Product
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
        }
    }
}
