using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Dto.Order
{
    public class OrderListItemDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public int SendPointId { get; set; }
        public string SendPointCode { get; set; }
        public string SendPointName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string BuyerEmail { get; set; }
        public int BuyerId { get; set; }
        public IList<OrderLineDto> Lines { get; set; }
    }
}
