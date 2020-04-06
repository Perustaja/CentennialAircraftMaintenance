using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains information linking hours of labor to an Employee
    /// </summary>
    public class LaborRecord
    {
        public int Id { get; private set; }
        // Discrepancy FK
        public int DiscrepancyId { get; private set; }
        // Employee FK
        public int EmployeeId { get; private set; }
        // Main
        [Display(Name = "Labor(Hours)")]
        public decimal LaborInHours { get; private set; }
        public Employee Employee { get; private set; }
        private LaborRecord()
        {
            // Required by EF
        }
        public LaborRecord(int discrepId, int employeeId, decimal laborInHours)
        {
            DiscrepancyId = discrepId;
            EmployeeId = employeeId;
            LaborInHours = laborInHours;
        }
        public void ChangeLaborHours(decimal laborInHours) => LaborInHours = laborInHours;
    }
}