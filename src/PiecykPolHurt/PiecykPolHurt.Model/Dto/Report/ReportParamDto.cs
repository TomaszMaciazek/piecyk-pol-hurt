using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Dto.Report
{
    public class ReportParamDto
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public ParamType Type { get; set; }
        public bool IsRequired { get; set; }
        public IEnumerable<ReportParamValueDto> AvailableValues { get; set; }
    }
}
