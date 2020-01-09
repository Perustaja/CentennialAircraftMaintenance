using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Parts
{
    public class PartsCreateViewModel
    {
        public int Id { get; set; }
        // PartCategory FK
        public int PartCategoryId { get; set; }
        // Main
        [Required]
        [StringLength(40)]
        public string PartNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public int CurrentStock { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public int? MinimumStock { get; set; } = 0;
    }
}