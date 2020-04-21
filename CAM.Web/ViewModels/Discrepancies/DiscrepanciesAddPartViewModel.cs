using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.ViewModels.Discrepancies
{
    public class DiscrepanciesAddPartViewModel
    {
        public int DiscrepancyId { get; set; }
        // Hidden part id set by js
        public int PartId { get; set; }
        [Required(ErrorMessage = "The part number cannot be empty")]
        // Client side validation for existing part
        [Remote(action: "VerifyPartExists", controller: "Inventory")]
        public string InputPartNumber { get; set; }

        [Range(1, 99, ErrorMessage = "Cannot be under 1")]
        public int InputQuantity { get; set; }
    }
}