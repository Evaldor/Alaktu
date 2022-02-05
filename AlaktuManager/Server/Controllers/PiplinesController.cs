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

        // GET: api/[controller]/view
        [HttpGet("View")]
        public async Task<ActionResult<IEnumerable<Pipline>>> GetView()
        {
            return await repository.GetView();
        }
    }
}