using System.Runtime.CompilerServices;
using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains information used for maintenance documents and tracking purposes. Its data is independent of 
    /// others, allowing it to serve as a snapshot and be edited as desired.
    /// </summary>
    public class DiscrepancyDto
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
        [Range(1900, 3000)]
        public int? Year { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        // Times properties
        public decimal Hobbs { get; set; }
        [Display(Name = "Air Time")]
        public int AirTime { get; set; }
        [Display(Name = "Tach 1")]
        public decimal Tach1 { get; set; }
        [Display(Name = "Tach 2")]
        public decimal Tach2 { get; set; }
        [Display(Name = "Prop 1")]
        public decimal Prop1 { get; set; }
        [Display(Name = "Prop 2")]
        public decimal Prop2 { get; set; }
        [Display(Name = "Aircraft Total")]
        public decimal AircraftTotal { get; set; }
        [Display(Name = "Engine 1 Total")]
        public decimal Engine1Total { get; set; }
        [Display(Name = "Engine 2 Total")]
        public decimal Engine2Total { get; set; }
        public int Cycles { get; set; }
    }
}