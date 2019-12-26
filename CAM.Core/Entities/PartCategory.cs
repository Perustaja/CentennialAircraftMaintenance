using System;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data relating to categories used for parts.
    /// </summary>
    public class PartCategory : BaseEntity<int>
    {
        public override int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}