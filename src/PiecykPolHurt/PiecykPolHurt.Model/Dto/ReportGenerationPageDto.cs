using PiecykPolHurt.Model.Dto.Report;

namespace PiecykPolHurt.Model.Dto
{
    public class ReportGenerationPageDto
    {
        public int ReportId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxRow { get; set; }
        public IEnumerable<ReportParamDto> Params { get; set; }
    }
}
