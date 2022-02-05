using AlaktuManager.Shared;

namespace AlaktuManager.Server.Repository.EFCore
{
    public class EfCoreSourceTypeRepository : EfCoreRepository<SourceType, AlaktuManagerServerContext>
    {
        public EfCoreSourceTypeRepository(AlaktuManagerServerContext context) : base(context)
        {

        }
        // We can add new methods specific to this repository here in the future
    }
}