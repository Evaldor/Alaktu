using AlaktuManager.Shared;

namespace AlaktuManager.Client.Services
{
    public class SourceTypeService : AlaktuManagerClientService<SourceType>
    {
        public SourceTypeService(HttpClient httpClient) : base(httpClient)
        {

        }

        public override string GetApiPath()
        {
            return "api/SourceTypes";
        }
    }
}
