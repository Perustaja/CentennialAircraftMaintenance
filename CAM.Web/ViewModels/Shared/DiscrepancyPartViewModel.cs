using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(DiscrepancyPart), ReverseMap = true)]
    public class DiscrepancyPartViewModel
    {
        public int PartId { get; set; }
        public int DiscrepancyId { get; set; }
        public string MfrsPartNumber { get; set; }
        public string Name { get; set; }
        public string ImageThumbPath { get; set; }
        [Range(0, 99)]
        public int Qty { get; set; }
    }
}