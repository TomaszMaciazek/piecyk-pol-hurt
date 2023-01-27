using AutoMapper;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.Report;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Mappings.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportDefinition, ReportListItemDto>();
            CreateMap<ReportDefinition, ReportDefinitionDto>();
            CreateMap<CreateReportDefinitionCommand, ReportDefinition>()
                .ForMember(x => x.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => "1.0"));
        }
    }
}
