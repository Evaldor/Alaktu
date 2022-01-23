using AlaktuManager.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AlaktuManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PiplineController : Controller
    {
        private readonly ILogger<PiplineController> _logger;

        public PiplineController(ILogger<PiplineController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Pipline> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Pipline
            {
                Id = index
            })
            .ToArray();
        }
    }
}
