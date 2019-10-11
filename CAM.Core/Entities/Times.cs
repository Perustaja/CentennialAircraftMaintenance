using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains aircraft time information. AircraftId is NOT a FK of Aircraft. The naming seems proper and is kept for ease of use when programming.
    /// However, Aircraft.Id is a FK based on Times.AircraftId. This is done to prevent errors if a plane is added quickly and 
    /// the times scraper tries to add a Times row for a nonexistant Aircraft.
    /// </summary>
    public class Times : TimesHolder
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        // Aircraft
        public Aircraft Aircraft { get; set; }
    }
}