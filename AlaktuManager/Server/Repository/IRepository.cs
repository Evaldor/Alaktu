using AlaktuManager.Shared;

namespace AlaktuManager.Server.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(Int64 id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Int64 id);
        Task<List<T>> GetView();
    }
}