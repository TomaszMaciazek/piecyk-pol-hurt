using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface ISendPointRepository : IRepository<SendPoint> {
        Task<bool> WasSendPointUsedInOrder(int id);
    }

    public class SendPointRepository : BaseRepository<SendPoint>, ISendPointRepository
    {
        public SendPointRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> WasSendPointUsedInOrder(int id)
            =>  await _context.Orders.AnyAsync(x => x.SendPointId == id);
    }
}
