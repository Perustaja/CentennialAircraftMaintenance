using System;
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
        [Range(0.1d, 99d)]
        public decimal LaborInHours { get; private set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DatePerformed { get; private set; }
        public Employee Employee { get; private set; }
        private LaborRecord()
        {
            // Required by EF
        }
        public LaborRecord(int discrepId, int employeeId, decimal laborInHours, DateTime date)
        {
            DiscrepancyId = discrepId;
            EmployeeId = employeeId;
            LaborInHours = laborInHours;
            DatePerformed = date.Date;
        }
        public void ChangeLaborHours(decimal laborInHours)
        {
            if (laborInHours > 99m)
                throw new ArgumentException("Hours cannot exceed 99.");
            LaborInHours = laborInHours;
        }
        public void AddLaborHours(decimal laborInHours)
        {
            if (LaborInHours + laborInHours > 99m)
                throw new ArgumentException("Hours cannot exceed 99.");
            LaborInHours += laborInHours;
        }
    }
}