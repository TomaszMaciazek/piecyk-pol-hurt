namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface IRepository<T> : IReadOnlyRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void RemoveRange(IEnumerable<T> entities);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
