using System;
using System.ComponentModel.DataAnnotations;
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "The part number must be a string consisting of normal characters and cannot be empty.")]
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "The given description cannot exceed 600 characters.")]
        [StringLength(600)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The price must be a valid number.")]
        [Display(Name = "Price In")]
        public decimal PriceIn { get; set; }

        [Display(Name = "Price Out")]
        public decimal? PriceOut { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The vendor's name cannot exceed 30 characters.")]
        [StringLength(30)]
        public string Vendor { get; set; }

        [Display(Name = "Minimum Stock Level")]
        public int? MinimumStock { get; set; }

        [IgnoreMap]
        [Display(Name = "Part Image")]
        [DataType(DataType.Upload)]
        [MaxFileSizeInBytes(5 * 1024 * 1024)]
        [AllowedFileExtensions(".jpg", ".png")]
        public IFormFile Image { get; set; }
    }
}