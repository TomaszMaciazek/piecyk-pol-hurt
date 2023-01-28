using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Queries
{
    public sealed class ReportQuery : ListQuery
    {
        public string Title { get; set; }
        public string Group { get; set; }
        public ReportSortOption? SortOption { get; set; }
    }
}
