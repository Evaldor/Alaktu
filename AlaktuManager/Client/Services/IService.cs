using AlaktuManager.Shared;

namespace AlaktuManager.Client.Services
{ 
    public interface IService<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Int64 id);
        Task<string> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(Int64 id);
        Task<IEnumerable<T>> GetView();
    }
}
