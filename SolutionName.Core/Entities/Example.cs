using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutionName.Core.Entities
{
    public class Example : BaseEntity
    {
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }
    }
}
