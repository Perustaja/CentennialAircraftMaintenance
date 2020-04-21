using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(LaborRecord), ReverseMap = true)]
    public class LaborRecordViewModel
    {
        public int Id { get; private set; }
        public int DiscrepancyId { get; private set; }
        public int EmployeeId { get; private set; }
        [Display(Name = "Labor(Hours)")]
        public decimal LaborInHours { get; private set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DatePerformed { get; private set; }
        public Employee Employee { get; private set; }
    }
}