using System.ComponentModel.DataAnnotations;

namespace AlaktuManager.Shared
{
    public class SourceType : IEntity
    {
        [Required]
        public Int64 Id { get; set; }
        public string? Name { get; set; }
    }
}
