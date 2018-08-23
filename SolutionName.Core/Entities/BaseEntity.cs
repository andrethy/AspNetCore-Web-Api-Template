using System.ComponentModel.DataAnnotations;

namespace SolutionName.Core.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}
