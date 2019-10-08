using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains information linking hours of labor to an Employee
    /// </summary>
    public class LaborRecord
    {
        public int Id { get; set; }
        // Discrepancy FK
        public int DiscrepancyId { get; set; }
        // Employee FK
        public int EmployeeId { get; set; }
        // Main
        [Display(Name = "Labor(Hours)")]
        public decimal LaborInHours { get; set; }
        // Discrepancy 
        public Discrepancy Discrepancy { get; set; }
        // Employee 
        public Employee Employee { get; set; }
    }
}