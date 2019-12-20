using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Discrepancies
{
    public class DiscrepanciesIndexViewModel
    {
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Title { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        public DateTime DateFinalized { get; set; }

        [Display(Name = "Awaiting Review")]
        public bool AwaitingFinalize { get; set; } = false;

        [Display(Name = "Is Finalized")]
        public bool IsFinalized { get; set; } = false;

        [Display(Name = "Created by")]
        [StringLength(60)]
        public string CreatedBy { get; set; }

        // Squawk properties
        [Required]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Resolution { get; set; }

        // Aircraft properties
        [StringLength(20)]
        [Display(Name = "Registration")]
        public string AircraftId { get; set; }
    }
}