using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains aircraft time information.
    /// </summary>
    public class Times : TimesHolder
    {
        [Key]
        [ForeignKey("Aircraft")]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        // Aircraft
        public Aircraft Aircraft { get; set; }
    }
}