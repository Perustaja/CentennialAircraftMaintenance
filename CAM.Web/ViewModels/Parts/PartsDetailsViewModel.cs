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
        public string ImagePath => $"~/img/parts/{Id.ToUpper()}.jpg";
        public int CurrentStock { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal PriceIn { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? PriceOut { get; set; }

        [Required]
        public string Vendor { get; set; }
        public bool IsDiscontinued { get; set; } = false;
        public int MinimumStock { get; set; }
        // Category 
        [SourceMember(nameof(Part.PartCategory))]
        public PartCategoryViewModel PartCategoryViewModel { get; set; }

        // View-only data
        [IgnoreMap]
        public string StockStatus
        {
            get
            {
                var status = "green";
                if (CurrentStock <= 0)
                    status = "red";
                else if (MinimumStock > CurrentStock)
                    status = "yellow";
                return status;
            }
        }

        [Ignore]
        public string StockColor
        {
            get
            {
                switch (StockStatus)
                {
                    case "green":
                        return "#006600";
                    case "yellow":
                        return "#cccc00";
                    default:
                        return "#b30000";
                }
            }
        }

        public string StockIcon
        {
            get
            {
                switch (StockStatus)
                {
                    case "green":
                        return "fas fa-check-circle";
                    default:
                        return "fas fa-exclamation-circle";
                }
            }
        }

        public string StockMessage
        {
            get
            {
                switch (StockStatus)
                {
                    case "green":
                        return "This item is currently in stock.";
                    case "yellow":
                        return "This item is in stock, but is below the recommended minimum stock level.";
                    default:
                        return "This item is currently out of stock.";
                }
            }
        }
    }
}