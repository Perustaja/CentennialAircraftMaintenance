using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Shared;

namespace CAM.Web.ViewModels.Parts
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartsDetailsViewModel
    {
        [Display(Name = "Manufacturer's Part Number")]
        public string Id { get; set; }
        [Display(Name = "Part Number")]
        public string CataloguePartNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImagePath => $"~/img/parts/{CataloguePartNumber.ToUpper()}";
        public int CurrentStock { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }

        [Required]
        public string Vendor { get; set; }
        public bool IsDiscontinued { get; set; } = false;
        public int MinimumStock { get; set; }
        // Category 
        [SourceMember(nameof(Part.PartCategory))]
        public PartCategoryViewModel PartCategoryViewModel { get; set; }
    }
}