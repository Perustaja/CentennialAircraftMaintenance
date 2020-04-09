using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Manufacturer's Part #")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string MfrsPartNumber { get; set; }
        public int PartCategoryId { get; set; }
        [Display(Name = "IPC Part #")]
        [StringLength(50)]
        public string CataloguePartNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(40)]
        public string Name { get; set; }
        [Range(0, 10000)]
        public int CurrentStock { get; set; }
        [Required(ErrorMessage = "The price must be a valid number.")]
        [Display(Name = "Price In")]
        [Range(0.01d, 100000d)]
        public decimal PriceIn { get; set; }
        [Required(ErrorMessage = "The price must be a valid number.")]
        [Display(Name = "Price In")]
        [Range(0.01d, 200000d)]
        public decimal? PriceOut { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Vendor { get; set; }
        [Range(0, 1000)]
        public int? MinimumStock { get; set; }

        [SourceMember(nameof(Part.PartCategory))]
        public PartCategoryViewModel PartCategoryViewModel { get; set; }
    }
}