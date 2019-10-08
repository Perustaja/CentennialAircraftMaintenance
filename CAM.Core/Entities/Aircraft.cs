using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities 
{
    /// <summary>
    /// Contains Aircraft data, no times currently.
    /// </summary>
    public class Aircraft
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Registration")]
        public string Id { get ; set; }
        //Main
        [StringLength(100)]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; } = "~/img/logo.png";

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
        public virtual ICollection<Squawk> Squawks { get; set; }
    }
}