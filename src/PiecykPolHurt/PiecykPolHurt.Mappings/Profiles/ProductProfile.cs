using AutoMapper;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, SimpleProductDto>();
        }
    }
}
