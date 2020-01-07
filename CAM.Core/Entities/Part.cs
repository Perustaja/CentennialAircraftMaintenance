using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using CAM.Core.Entities.DiscrepancyAggregate;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains general item information used for inventory purposes. 
    /// </summary>
    public class Part : BaseEntity<int>
    {
        public override int Id { get; set; }
        // PartCategory FK
        public int PartCategoryId { get; set; }
        // Main
        [Required]
        [StringLength(40)]
        public string PartNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public string ImagePath => $"~/img/parts/{PartNumber.ToUpper()}";
        public int CurrentStock { get; set; }
        public int QtySoldToDate { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public int? MinimumStock { get; set; } = 0;
        // Category 
        public PartCategory PartCategory { get; set; }
        // Required by EF
        public List<DiscrepancyPart> DiscrepancyParts { get; set; }
    }
}