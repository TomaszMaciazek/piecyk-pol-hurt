using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Queries
{
    public class ProductQuery : ListQuery
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public ProductSortOption? SortOption { get; set; }
    }
}
