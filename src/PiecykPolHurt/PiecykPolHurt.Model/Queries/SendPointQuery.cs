using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Queries
{
    public class SendPointQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public SendPointSortOption? SortOption { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
