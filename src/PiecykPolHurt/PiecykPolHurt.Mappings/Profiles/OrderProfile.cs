using AutoMapper;
using PiecykPolHurt.Model.Dto.Order;
using PiecykPolHurt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderLine, OrderLineDto>()
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderListItemDto>()
                .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.Buyer.Email))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.SendPointCode, opt => opt.MapFrom(src => src.SendPoint.Code))
                .ForMember(dest => dest.SendPointName, opt => opt.MapFrom(src => src.SendPoint.Name))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Lines.Select(x => x.PriceForOneItem * x.ItemsQuantity).Sum()));

            CreateMap<Order, OrderDto>();
        }
    }
}
