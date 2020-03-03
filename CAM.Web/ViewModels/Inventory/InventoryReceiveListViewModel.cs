using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Inventory
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class InventoryReceiveListViewModel
    {
        public string PartNumber { get; set; }
        public int Quantity { get; set; }
        public int StockCount { get; set; }
        public string Category { get; set; }
        [IgnoreMap]
        public string ImgThumb { get; set; }
        [IgnoreMap]
        public int Qty { get; set; }
    }
}