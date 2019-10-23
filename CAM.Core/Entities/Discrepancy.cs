using System.Runtime.CompilerServices;
using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains information used for maintenance documents and tracking purposes. Its data is independent of 
    /// others, allowing it to serve as a snapshot and be edited as desired.
    /// </summary>
    public class Discrepancy : TimesHolder
    {
        public int Id { get; set; }
        // WorkOrder FK
        public int WorkOrderId { get; set; }
        // Main
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
        public string CreatedBy {get; set; }
        // Squawk properties
        [Required]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Resolution { get; set; }
        // Aircraft properties
        [Range(1900, 3000)]
        public int? Year { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        // WorkOrder 
        public WorkOrder WorkOrder { get; set; }
        // Navigation properties
        public ICollection<LaborRecord> LaborRecords { get; set; }
        public ICollection<DiscrepancyPart> DiscrepancyParts { get; set; }

    }
}