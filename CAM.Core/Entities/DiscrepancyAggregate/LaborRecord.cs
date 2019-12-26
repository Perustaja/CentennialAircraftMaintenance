using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities.DiscrepancyAggregate
{
    /// <summary>
    /// Contains information linking hours of labor to an Employee
    /// </summary>
    public class LaborRecord : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Discrepancy FK
        public int DiscrepancyId { get; set; }
        // Employee FK
        public int EmployeeId { get; set; }
        // Main
        [Display(Name = "Labor(Hours)")]
        public decimal LaborInHours { get; set; }
        public Employee Employee { get; set; }
    }
}