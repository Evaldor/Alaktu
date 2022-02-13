using AlaktuManager.Shared;
using System.Net.Http.Json;

namespace AlaktuManager.Client.Services
{
    public abstract class AlaktuManagerClientService<TEntity> : IService<TEntity>
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

        public async Task<IEnumerable<TEntity>> GetView()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TEntity>>(apiPath + "/view");
        }

        public async Task<TEntity> Get(Int64 id)
        {
            return await httpClient.GetFromJsonAsync<TEntity>(apiPath + "/" + id);
        }

        public async Task<string> Add(TEntity entity)
        {
            var response = await httpClient.PostAsJsonAsync<TEntity>(apiPath, entity);
        
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            httpClient.PutAsJsonAsync<TEntity>(apiPath + "/" + entity.Id.ToString(), entity);

            return await httpClient.GetFromJsonAsync<TEntity>(apiPath + "/" + entity.Id.ToString());
        }

        public async Task Delete(Int64 id)
        {
            await httpClient.DeleteAsync(apiPath + "/" + id);
        }
    }
}