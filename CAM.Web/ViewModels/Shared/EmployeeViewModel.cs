using System;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(Employee), ReverseMap = true)]
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CertificationNum { get; set; }
        public string Initials => $"{Char.ToUpper(FirstName[0])}{LastName}";
    }
}