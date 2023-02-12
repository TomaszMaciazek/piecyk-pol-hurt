﻿using AutoMapper;
using PiecykPolHurt.Model.Commands;
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
    }
}