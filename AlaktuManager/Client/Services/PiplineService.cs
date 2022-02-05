using AlaktuManager.Shared;

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
    }
}
