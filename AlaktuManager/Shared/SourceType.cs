using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlaktuManager.Shared
{
    public class SourceType : IEntity
    {
        [Key]
        public Int64 Id { get; set; }
        [StringLength(500)]
        public string? Name { get; set; }
    }
}
