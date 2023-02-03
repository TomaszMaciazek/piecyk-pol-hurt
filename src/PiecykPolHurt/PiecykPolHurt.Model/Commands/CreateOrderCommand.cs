namespace PiecykPolHurt.Model.Commands
{
    public class CreateOrderCommand
    {
        public DateTime RequestedReceptionDate { get; set; }
        public ICollection<CreateOrderLineCommand> Lines { get; set; }
        public int SendPointId { get; set; }
    }
}
