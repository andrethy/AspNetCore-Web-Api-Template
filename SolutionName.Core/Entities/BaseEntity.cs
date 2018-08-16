﻿using System.ComponentModel.DataAnnotations;

namespace SolutionName.Core.Entities
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}
