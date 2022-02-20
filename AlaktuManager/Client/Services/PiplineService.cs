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
            return "api/piplines";
        }

    }
}
