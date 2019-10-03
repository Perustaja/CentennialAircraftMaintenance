using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains Aircraft data, no times currently.
    /// </summary>
    public class Aircraft
    {
        [Key, Required]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string Id { get; set; }
        //Main
        [StringLength(100)]
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