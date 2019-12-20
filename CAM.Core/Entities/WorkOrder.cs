using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Used to organize Discrepancies, acting like a container. 
    /// </summary>
    public class WorkOrder : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Main
        [Required]
        [StringLength(15)]
        public string Title  { get; set; }

        [StringLength(20)]
        [Display(Name = "Registration")]
        public string AircraftId { get; set; }

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
    }
}