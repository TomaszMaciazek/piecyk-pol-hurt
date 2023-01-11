namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface IOrderRepository: IRepository<Order> { }
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
