using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface ISendPointRepository : IRepository<SendPoint> {
        Task<bool> WasSendPointUsedInOrder(int id);
        Task<SendPoint> GetByCode(String code);
    }

    public class SendPointRepository : BaseRepository<SendPoint>, ISendPointRepository
    {
        public SendPointRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> WasSendPointUsedInOrder(int id)
            =>  await _context.Orders.AnyAsync(x => x.SendPointId == id);

        public async Task<SendPoint> GetByCode(String code)
            => await _context.SendPoints.FirstAsync(x => x.Code.Equals(code));
    }
}
