using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains general item information used for inventory purposes. 
    /// </summary>
    public class Part
    {
        public int Id { get; set; }
        // Category FK
        public int CategoryId { get; set; }
        // Main
        [Required]
        [StringLength(40)]
        public string PartNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public int QtyCurrent { get; set; }
        public int QtySoldYear { get; set; }
        public decimal PriceIn { get; set; }
        public decimal PriceOut { get; set; }
        public int MinimumStock { get; set; }
        // Category 
        public Category Category { get; set; }
        // Navigation properties
        public virtual ICollection<DiscrepancyPart> DiscrepancyParts { get; set; }

    }
}