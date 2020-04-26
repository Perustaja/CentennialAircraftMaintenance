using System;
using System.ComponentModel.DataAnnotations;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Discrepancies
{
    public class DiscrepanciesAddLaborViewModel
    {
        public int DiscrepancyId { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [Display(Name = "Labor(Hours)")]
        [Range(0.1d, 99d, ErrorMessage = "Must be greater than 1.")]
        public decimal LaborInHours { get; private set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DatePerformed { get; private set; }
    }
}