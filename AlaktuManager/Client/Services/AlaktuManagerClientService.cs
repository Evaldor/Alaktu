using AlaktuManager.Shared;
using System.Net.Http.Json;

namespace AlaktuManager.Client.Services
{
    public abstract class AlaktuManagerClientService<TEntity>  :IService<TEntity>
        where TEntity : class, IEntity
    {
        public readonly HttpClient httpClient;
        public readonly string apiPath;
        public AlaktuManagerClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiPath = GetApiPath();
        }

        public virtual string GetApiPath()
        {
            return "";
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TEntity>>(apiPath);
        }

        public virtual async Task<IEnumerable<TEntity>> GetView()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(Int64 id)
        {
            throw new NotImplementedException();
        }
        public Task<TEntity> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Int64 id)
        {
            throw new NotImplementedException();
        }
    }
}