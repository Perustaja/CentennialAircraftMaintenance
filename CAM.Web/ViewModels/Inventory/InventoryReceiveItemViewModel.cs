using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Inventory
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class InventoryReceiveItemViewModel
    {
        public string Id { get; set; }
        public string CataloguePartNumber { get; set; }
        public string Name { get; set; }
        public int CurrentStock { get; set; }
        public string ImageThumbPath { get; set; }
        [IgnoreMap]
        public int Qty { get; set; }
    }
}