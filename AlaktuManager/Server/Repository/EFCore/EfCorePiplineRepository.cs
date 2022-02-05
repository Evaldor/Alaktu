using AlaktuManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace AlaktuManager.Server.Repository.EFCore
{
    public class EfCorePiplineRepository : EfCoreRepository<Pipline, AlaktuManagerServerContext>
    {
        public EfCorePiplineRepository(AlaktuManagerServerContext context) : base(context)
        {

        }
        // We can add new methods specific to this repository here in the future
        public override async Task<List<Pipline>> GetView()
        {
            return await context.Pipline.Include("SourceType").ToListAsync();
        }
    }
}