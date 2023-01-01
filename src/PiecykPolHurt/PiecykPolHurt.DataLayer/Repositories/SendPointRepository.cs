using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface ISendPointRepository : IReadOnlyRepository<SendPoint> { }

    public class SendPointRepository : BaseRepository<SendPoint>, ISendPointRepository
    {
        public SendPointRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
