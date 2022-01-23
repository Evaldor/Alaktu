using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaktuManager.Shared
{
    public class Pipline
    {
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public Int32 SourceTypeId { get; set; }
        public string? SourseConnection { get; set; }
        public string? SourseCredentials { get; set; }
        public DateTime? LastProcessedAt { get; set; }
        public byte[]? ActualRowVersion { get; set; }
        public bool? PrimaryKeyIsUsed { get; set; }
        public string? PrimaryKeySchemeJson { get; set; }
        public bool? VersioningIsUsed { get; set; }
        public string? VersioningSchemeJson { get; set; }
        public bool? TransferToFlatTableIsEnabled { get; set; }
        public string? TransferToFlatTableSchemeJson { get; set; }
        public string? LastProcessedStatus { get; set; }
        public Int32 BatchSize { get; set; }
        public Int32 CurrentRow { get; set; }
        public string? SourceTypeSettingsJson { get; set; }
    }
}
