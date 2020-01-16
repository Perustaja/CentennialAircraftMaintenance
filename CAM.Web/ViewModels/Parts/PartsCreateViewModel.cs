using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using CAM.Core.Entities.DiscrepancyAggregate;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CAM.Core.Entities;
using CAM.Core.Attributes;

namespace CAM.Web.ViewModels.Parts
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartsCreateViewModel
    {
        [Display(Name = "Manufacturer's Part #")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Id { get; set; }

        // PartCategory FK
        [Display(Name = "Category")]
        public int PartCategoryId { get; set; }

        // Main
        [Display(Name = "IPC Part #")]
        [StringLength(50)]
        public string CataloguePartNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(40)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(600)]
        public string Description { get; set; }

        [Display(Name = "Price In")]
        public decimal PriceIn { get; set; }

        [Display(Name = "Price Out")]
        public decimal? PriceOut { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string Vendor { get; set; }

        [Display(Name = "Minimum Stock Level")]
        public int? MinimumStock { get; set; }

        [IgnoreMap]
        [Display(Name = "Part Image")]
        [DataType(DataType.Upload)]
        [MaxFileSizeInBytes(1024 * 5)]
        [AllowedFileExtensions()]
        public IFormFile Image { get; set; }
    }
}