using System;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Discrepancies
{
    public class DiscrepanciesAddLaborViewModel
    {
        public int DiscrepancyId { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [Display(Name = "Hours Worked")]
        [Range(0.1d, 99d, ErrorMessage = "Must be a positive number between 0 and 99.")]
        public decimal LaborInHours { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DatePerformed { get; set; }
    }
}