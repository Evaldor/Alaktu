using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlaktuManager.Shared
{
    public class Pipline : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        public Int64 SourceTypeId { get; set; }
        public SourceType? SourceType { get; set; }
        public string? SourseConnection { get; set; }
        public string? SourseCredentials { get; set; }
        [DefaultValue(false)]
        public bool IsEnabled { get; set; }
        public DateTime? LastProcessedAt { get; set; }
        public byte[]? ActualRowVersion { get; set; }
        [DefaultValue(false)]
        public bool PrimaryKeyIsUsed { get; set; }
        public string? PrimaryKeySchemeJson { get; set; }
        [DefaultValue(false)]
        public bool VersioningIsUsed { get; set; }
        public string? VersioningSchemeJson { get; set; }
        [DefaultValue(false)]
        public bool TransferToFlatTableIsEnabled { get; set; }
        public string? TransferToFlatTableSchemeJson { get; set; }
        public string? LastProcessedStatus { get; set; }
        [DefaultValue(100000)]
        public Int32 BatchSize { get; set; }
        [DefaultValue(0)]
        public Int32 CurrentRow { get; set; }
        public string? SourceTypeSettingsJson { get; set; }    
    }
}
