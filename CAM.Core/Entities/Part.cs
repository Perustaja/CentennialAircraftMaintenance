using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains general item information used for inventory purposes.
    /// </summary>
    public class Part
    {
        public int Id { get; private set; }
        // PartCategory FK
        public int PartCategoryId { get; private set; }
        // Main
        [Display(Name = "Manufacturer's Part #")]
        [StringLength(50, MinimumLength = 4)]
        public string MfrsPartNumber { get; private set; }
        [Display(Name = "IPC Part #")]
        [StringLength(50)]
        public string CataloguePartNumber { get; private set; }
        [Required]
        [StringLength(40)]
        public string Name { get; private set; }
        [Required]
        [StringLength(600)]
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string ImageThumbPath { get; private set; }
        public int CurrentStock { get; private set; }
        public decimal PriceIn { get; private set; }
        public decimal? PriceOut { get; private set; }
        [Required]
        [StringLength(30)]
        public string Vendor { get; private set; }
        public int? MinimumStock { get; private set; }
        // Category 
        public PartCategory PartCategory { get; private set; }
        // Required by EF for join table creation, will not be accessed
        public List<DiscrepancyPart> DiscrepancyParts { get; private set; }
        public List<DiscrepancyTemplatePart> DiscrepancyTemplateParts { get; private set; }
        // Methods
        private Part()
        {
            // Required by EF
        }
        public Part(string mfrPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock)
        {
            MfrsPartNumber = mfrPartNumber;
            PartCategoryId = partCategoryId;
            CataloguePartNumber = cataloguePartNumber ?? mfrPartNumber;
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
        public void EditPart(string mfrPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock)
        {
            MfrsPartNumber = mfrPartNumber;
            PartCategoryId = partCategoryId;
            CataloguePartNumber = cataloguePartNumber ?? mfrPartNumber;
            Name = name;
            Description = description;
            // Figure out setting of image
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
                throw new ArgumentOutOfRangeException("Part: Attempted to add or remove stock using an invalid quantity");
        }
    }
}