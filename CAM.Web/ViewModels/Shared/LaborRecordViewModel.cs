using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(LaborRecord), ReverseMap = true)]
    public class LaborRecordViewModel
    {
        public int EmployeeId { get; set; }
        public string CertificationNum { get; set; }
        [Display(Name = "Labor(Hours)")]
        public decimal LaborInHours { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DatePerformed { get; set; }
        [SourceMember(nameof(Employee.Initials))]
        public string Initials { get; set; }
    }
}