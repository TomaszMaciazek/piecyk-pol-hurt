using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
