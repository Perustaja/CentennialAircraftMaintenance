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
        public string MfrsPartNumber { get; set; }
        public int PartCategoryId { get; set; }
        [Display(Name = "IPC Part #")]
        public string CataloguePartNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public int CurrentStock { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public int? MinimumStock { get; set; }

        [SourceMember(nameof(Part.PartCategory))]
        public PartCategoryViewModel PartCategoryViewModel { get; set; }
    }
}