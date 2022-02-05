using AlaktuManager.Shared;
using System.Net.Http.Json;

namespace AlaktuManager.Client.Services
{
    public class PiplineService : AlaktuManagerClientService<Pipline>
    {
        public PiplineService(HttpClient httpClient) : base(httpClient)
        {

        }

        public override string GetApiPath()
        {
            return "api/Piplines";
        }

        public override async Task<IEnumerable<Pipline>> GetView()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Pipline>>(apiPath+"/view");
        }
    }
}
