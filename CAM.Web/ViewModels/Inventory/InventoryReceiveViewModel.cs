using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.ViewModels.Inventory
{
    public class InventoryReceiveViewModel
    {
        [Required(ErrorMessage = "The part number cannot be empty")]
        // Client side validation for existing part
        [Remote(action:"VerifyPartExists", controller:"Inventory")]
        public string InputPartNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1")]
        public int InputQuantity { get; set; }
    }
}