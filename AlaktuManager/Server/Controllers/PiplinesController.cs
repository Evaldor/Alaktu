using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AlaktuManager.Server.Repository.EFCore;
using AlaktuManager.Shared;

namespace AlaktuManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiplinesController : AlaktuManagerServerController<Pipline, EfCorePiplineRepository>
    {
        public PiplinesController(EfCorePiplineRepository repository) : base(repository)
        {

        }
    }
}