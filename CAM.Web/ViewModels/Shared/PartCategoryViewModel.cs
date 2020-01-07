using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(PartCategory), ReverseMap = true)]
    public class PartCategoryViewModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}