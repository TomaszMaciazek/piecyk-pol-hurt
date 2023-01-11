namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface ISendPointRepository : IReadOnlyRepository<SendPoint> { }

    public class SendPointRepository : BaseRepository<SendPoint>, ISendPointRepository
    {
        public SendPointRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
