using Microsoft.AspNetCore.Mvc;
using AlaktuManager.Server.Repository.EFCore;
using AlaktuManager.Shared;

namespace AlaktuManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourceTypesController : AlaktuManagerServerController<SourceType, EfCoreSourceTypeRepository>
    {
        public SourceTypesController(EfCoreSourceTypeRepository repository) : base(repository)
        {

        }
    }
}
