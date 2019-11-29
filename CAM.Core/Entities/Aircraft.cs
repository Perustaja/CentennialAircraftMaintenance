using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities 
{
    /// <summary>
    /// Contains Aircraft data. Aircraft.Id is a FK of Times.AircraftId. Because Times is populated by an html scraper,
    /// it is important to realize that despite the naming conventions, Aircraft.Id is reliant upon Times.AircraftId or else there would be errors
    /// if an aircraft were added overnight for instance. Please keep this in mind, the naming convention makes sense but remember who actually follows whom.
    /// </summary>
    public class Aircraft : BaseEntity<string>
    {
        [ForeignKey("Times")]
        [StringLength(20)]
        [Display(Name = "Registration")]
        public override string Id { get ; set; }
        //Main
        [StringLength(100)]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; } = "default.png";

        [Range(1900, 3000)]
        public int? Year { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        [StringLength(30)]
        public string SerialNum { get; set; }
        public bool IsTwin { get; set; } = false;
        // Times 
        public Times Times { get; set; }
        // Navigation properties
        public List<Squawk> Squawks { get; set; }
        public List<AircraftOwner> AircraftOwners { get; set; }
    }
}