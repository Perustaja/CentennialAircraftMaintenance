using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using CAM.Core.Entities.DiscrepancyAggregate;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains general item information used for inventory purposes.
    /// </summary>
    public class Part
    {
        [Display(Name = "Manufacturer's Part #")]
        [Key]
        [StringLength(50, MinimumLength = 4)]
        public string Id { get; set; }
        // PartCategory FK
        public int PartCategoryId { get; set; }
        // Main
        [Display(Name = "IPC Part #")]
        [StringLength(50)]
        public string CataloguePartNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(600)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }
        public int CurrentStock { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        [Required]
        [StringLength(30)]
        public string Vendor { get; set; }
        public int? MinimumStock { get; set; }
        // Category 
        public PartCategory PartCategory { get; set; }
        // Required by EF
        public List<DiscrepancyPart> DiscrepancyParts { get; set; }
        // Methods
        private Part()
        {
            // Required by EF
        }
        public Part(string id, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock)
        {
            Id = id;
            PartCategoryId = partCategoryId;
            CataloguePartNumber = cataloguePartNumber ?? Id;
            Name = name;
            Description = description;
            ImagePath = Constants.DEFAULT_IMAGE_PATH;
            ImageThumbPath = Constants.DEFAULT_THUMB_PATH;
            CurrentStock = 0;
            PriceIn = priceIn;
            PriceOut = priceOut ?? PriceIn * Constants.PRICE_MARKUP;
            Vendor = vendor;
            MinimumStock = minimumStock ?? 0;
        }
        public void AddStock(int qty)
        {
            GuardAgainstNegative(qty);
            CurrentStock += qty;
        }
        public void RemoveStock(int qty)
        {
            GuardAgainstNegative(qty);
            if (qty > CurrentStock)
                CurrentStock = 0;
            else
                CurrentStock -= qty;
        }
        public void ChangeImage(string imagePath, string imageThumbPath)
        {
            ImagePath = imagePath;
            ImageThumbPath = imageThumbPath;
        }
        private void GuardAgainstNegative(int arg)
        {
            if (arg < 0)
                throw new ArgumentException("An invalid negative value was encountered.");
        }
    }
}