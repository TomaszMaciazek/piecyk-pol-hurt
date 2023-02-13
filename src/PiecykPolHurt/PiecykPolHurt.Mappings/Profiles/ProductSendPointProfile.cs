using AutoMapper;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.Product;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles;

public class ProductSendPointProfile : Profile
{
    public ProductSendPointProfile()
    {
        CreateMap<CreateProductSendPointCommand, ProductSendPoint>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.SendPoint, opt => opt.Ignore());
        
        CreateMap<UpdateProductSendPointCommand, ProductSendPoint>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.SendPoint, opt => opt.Ignore());

        CreateMap<ProductSendPoint, ProductSendPoint>()
            .ForMember(dest => dest.ForDate, opt => opt.Ignore());

        CreateMap<ProductSendPoint, ProductSendPointListItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Product.IsActive))
            .ForMember(dest => dest.AvailableQuantity, opt => opt.MapFrom(src => src.AvailableQuantity));
    }
}