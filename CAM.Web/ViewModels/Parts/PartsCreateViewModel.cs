using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CAM.Core.Entities;
using CAM.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.ViewModels.Parts
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartsCreateViewModel
    {
        [Display(Name = "Manufacturer's Part #")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 4)]
        // Client side validation for existing part
        [Remote(action: "VerifyPartNonExistent", controller: "Inventory")]
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

        [Required(ErrorMessage = "The price must be a valid number.")]
        [Range(0.01d, 100000d)]
        [Display(Name = "Price In")]
        public decimal PriceIn { get; set; }

        [Display(Name = "Price Out")]
        [Range(0.01d, 200000d)]
        public decimal? PriceOut { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Vendor { get; set; }

        [Display(Name = "Minimum Stock Level")]
        [Range(1, 1000)]
        public int? MinimumStock { get; set; }

        [IgnoreMap]
        [Display(Name = "Part Image")]
        [DataType(DataType.Upload)]
        [MaxFileSizeInBytes(5 * 1024 * 1024)]
        [AllowedFileExtensions(false, ".jpg", ".png")]
        public IFormFile Image { get; set; }
    }
}