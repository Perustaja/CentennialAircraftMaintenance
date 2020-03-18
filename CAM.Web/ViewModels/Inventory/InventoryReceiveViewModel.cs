using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.ViewModels.Inventory
{
    public class InventoryReceiveViewModel
    {
        [Required(ErrorMessage = "The part number cannot be empty")]
        // Client side validation for existing part
        [Remote(action: "VerifyPartExists", controller: "Inventory")]
        public string InputPartNumber { get; set; }

        [Range(1, 2000, ErrorMessage = "Cannot be under 1")]
        public int InputQuantity { get; set; }
    }
}