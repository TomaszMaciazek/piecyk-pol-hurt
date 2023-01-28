using AutoMapper;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, SimpleProductDto>();

            CreateMap<Product, ProductListItemDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Lines, opt => opt.Ignore())
                .ForMember(dest => dest.SendPoints, opt => opt.Ignore());

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Lines, opt => opt.Ignore())
                .ForMember(dest => dest.SendPoints, opt => opt.Ignore());
        }
    }
}
