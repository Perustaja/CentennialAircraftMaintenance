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
        public int Qty { get; set; }
        [SourceMember(nameof(DiscrepancyPart.Part))]
        public PartViewModel PartViewModel { get; set; }
    }
}