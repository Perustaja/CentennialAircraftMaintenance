using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels
{
    [AutoMap(typeof(WorkStatus), ReverseMap = true)]
    public class WorkStatusViewModel
    {
        [StringLength(15)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}