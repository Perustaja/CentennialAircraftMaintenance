using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Squawks
{
    public class SquawksEditViewModel
    {
        public int Id { get; set; }
        // Aircraft FK
        [Required]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        // Status FK
        public int StatusId { get; set; }
        // Main
        [StringLength(1000)]
        [Required]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Resolution { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Resolved")]
        [DataType(DataType.Date)]
        public DateTime? DateResolved { get; set; }
        [StringLength(30)]
        [Display(Name = "Resolved By")]
        public string ResolvedBy { get; set; }
        public bool IsGroundable { get; set; } = false;
        public List<string> AircraftIds { get; set; } 
    }
}