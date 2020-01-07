using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Shared;

namespace CAM.Web.ViewModels.Inventory
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class InventoryIndexViewModel
    {
        public int Id { get; set; }

        public int PartCategoryId { get; set; }

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
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public int? MinimumStock { get; set; }

        [SourceMember(nameof(Part.PartCategory))]
        public PartCategoryViewModel PartCategoryViewModel { get; set; }
    }
}