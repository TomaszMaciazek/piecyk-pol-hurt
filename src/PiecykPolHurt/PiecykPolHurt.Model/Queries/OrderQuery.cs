using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Queries
{
    public class OrderQuery : ListQuery
    {
        public IEnumerable<int> SendPoints { get; set; }
        public IEnumerable<OrderStatus> Statuses { get; set; }
        public OrderSortOption? SortOption { get; set; }
    }
}
