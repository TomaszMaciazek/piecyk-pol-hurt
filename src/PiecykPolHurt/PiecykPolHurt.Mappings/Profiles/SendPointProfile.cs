using AutoMapper;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class SendPointProfile : Profile
    {
        public SendPointProfile() {
            CreateMap<SendPoint, SendPointListItemDto>();
            CreateMap<SendPoint, SimpleSendPointDto>();
        }
    }
}
