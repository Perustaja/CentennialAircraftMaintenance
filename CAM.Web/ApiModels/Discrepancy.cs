using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains information used for maintenance documents and tracking purposes. Its data is independent of 
    /// others, allowing it to serve as a snapshot and be edited as desired.
    /// </summary>
    public class Discrepancy
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
        [Display(Name = "Is Finalized")]
        public bool IsFinalized { get; set; } = false;
        // Squawk properties
        [Required]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Resolution { get; set; }
        // Aircraft properties
        [Required]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        [Range(1900, 3000)]
        public int? Year { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        // Times properties
        public decimal Hobbs { get; set; }
        public int AirTime { get; set; }
        public decimal Tach1 { get; set; }
        public decimal Tach2 { get; set; }
        public decimal Prop1 { get; set; }
        public decimal Prop2 { get; set; }
        public decimal AircraftTotal { get; set; }
        public decimal Engine1Total { get; set; }
        public decimal Engine2Total { get; set; }
        public int Cycles { get; set; }
        // WorkOrder 
        public WorkOrder WorkOrder { get; set; }
        // Navigation properties
        public virtual ICollection<LaborRecord> LaborRecords { get; set; }
        public virtual ICollection<DiscrepancyPart> DiscrepancyParts { get; set; }

    }
}