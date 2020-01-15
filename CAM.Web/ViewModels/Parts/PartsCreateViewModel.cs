using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using CAM.Core.Entities.DiscrepancyAggregate;
using Microsoft.AspNetCore.Http;

namespace CAM.Web.ViewModels.Parts
{
    public class PartsCreateViewModel
    {
        [Display(Name = "Manufacturer's Part #")]
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        // PartCategory FK
        [Display(Name = "Category")]
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
        [Display(Name = "Part Image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        [Display(Name = "Price In")]
        public decimal PriceIn { get; set; }
        [Display(Name = "Price Out")]
        public decimal? PriceOut { get; set; }
        [Required]
        [StringLength(20)]
        public string Vendor { get; set; }
        [Display(Name = "Minimum Stock Level")]
        public int? MinimumStock { get; set; }
    }
}