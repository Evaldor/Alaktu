#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlaktuManager.Shared;

namespace AlaktuManager.Server.Repository.EFCore
{
    public class AlaktuManagerServerContext : DbContext
    {
        public AlaktuManagerServerContext (DbContextOptions<AlaktuManagerServerContext> options)
            : base(options)
        {
        }

        public DbSet<AlaktuManager.Shared.Pipline> Pipline { get; set; }
        public DbSet<AlaktuManager.Shared.SourceType> SourceType { get; set; }
    }
}
