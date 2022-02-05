﻿using AlaktuManager.Shared;

namespace AlaktuManager.Server.Repository.EFCore
{
    public class EfCorePiplineRepository : EfCoreRepository<Pipline, AlaktuManagerServerContext>
    {
        public EfCorePiplineRepository(AlaktuManagerServerContext context) : base(context)
        {

        }
        // We can add new methods specific to this repository here in the future
    }
}