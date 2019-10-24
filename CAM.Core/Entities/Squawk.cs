using System;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data purely for an organized front-end facing object representing a squawk. 
    /// </summary>
    public class Squawk : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Aircraft FK
        [Required]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        // Status FK
        public int StatusId { get; set; }
        // Main
        [StringLength(30)]
        [Required]
        public string Pilot { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [StringLength(1000)]
        [Required]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Resolution { get; set; }
        [Display(Name = "Date Resolved")]
        [DataType(DataType.Date)]
        public DateTime? DateResolved { get; set; }
        [StringLength(30)]
        [Display(Name = "Resolved By")]
        public string ResolvedBy { get; set; }
        public bool IsGroundable { get; set; } = false;
        // Aircraft 
        public Aircraft Aircraft { get; set; }
        // Status
        public Status Status { get; set; }
    }
}