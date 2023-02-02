using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public int BuyerId { get; set; }
        public DateTime RequestedReceptionDate { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderLineDto> Lines { get; set; }
        public int SendPointId { get; set; }
    }
}
