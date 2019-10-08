using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Core.SharedKernel
{
    public abstract class BaseEntity<T> 
    {
        [Required]
        [Key]
        public T Id { get; set; }
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}