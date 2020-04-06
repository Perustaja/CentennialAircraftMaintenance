using CAM.Core.Entities;
using AutoMapper;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains general item information used for inventory purposes. 
    /// </summary>
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartDto
    {
        public int Id { get; set; }
        // Main
        public string MfrsPartNumber { get; set; }
        public string CataloguePartNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentStock { get; set; }
        public int QtySoldToDate { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public bool IsDiscontinued { get; set; } = false;
        public int? MinimumStock { get; set; }
        // Category 
        public PartCategoryDto PartCategory { get; set; }
    }
}