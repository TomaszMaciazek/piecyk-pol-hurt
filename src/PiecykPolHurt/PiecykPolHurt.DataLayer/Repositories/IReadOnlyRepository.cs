namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface IReadOnlyRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetById(int id);
    }
}
