using AutoMapper;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class SendPointProfile : Profile
    {
        public SendPointProfile() {
            CreateMap<SendPoint, SendPointListItemDto>();
            CreateMap<SendPoint, SimpleSendPointDto>();
            CreateMap<SendPoint, SendPointDto>();

            CreateMap<CreateSendPointCommand, SendPoint>();
            CreateMap<UpdateSendPointCommand, SendPoint>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}
