using System.ComponentModel.DataAnnotations;

namespace SolutionName.Common.Model.Example
{
    public class CreateExampleModel
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }
    }
}
