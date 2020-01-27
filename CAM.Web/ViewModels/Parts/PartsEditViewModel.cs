using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Attributes;
using CAM.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CAM.Web.ViewModels.Parts
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartsEditViewModel
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

        [Required(ErrorMessage = "The price must be a valid number.")]
        [Display(Name = "Price In")]
        public decimal PriceIn { get; set; }

        [Display(Name = "Price Out")]
        public decimal? PriceOut { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Vendor { get; set; }

        [Display(Name = "Minimum Stock Level")]
        public int? MinimumStock { get; set; }

        [IgnoreMap]
        [Display(Name = "Part Image")]
        [DataType(DataType.Upload)]
        [MaxFileSizeInBytes(5 * 1024 * 1024)]
        [AllowedFileExtensions(false, ".jpg", ".png")]
        public IFormFile Image { get; set; }
    }
}