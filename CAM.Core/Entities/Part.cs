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
    public class Part : BaseEntity<string>
    {
        [Display(Name = "Manufacturer's Part #")]
        [Key]
        [StringLength(50)]
        public override string Id { get; set; }
        // PartCategory FK
        public int PartCategoryId { get; set; }
        // Main
        [Display(Name = "IPC Part #")]
        [StringLength(50)]
        public string CataloguePartNumber { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(600)]
        public string Description { get; set; }
        public string ImagePath => $"~/img/parts/{Id.ToUpper()}.jpg";
        public int CurrentStock { get; set; }
        public int QtySoldToDate { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }

        [Required]
        [StringLength(20)]
        public string Vendor { get; set; }
        public bool IsDiscontinued { get; set; } = false;
        public int? MinimumStock { get; set; } = 0;
        // Category 
        public PartCategory PartCategory { get; set; }
        // Required by EF
        public List<DiscrepancyPart> DiscrepancyParts { get; set; }
    }
}