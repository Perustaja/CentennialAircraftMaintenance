using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Inventory
{
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class InventoryReceiveListViewModel
    {
        public List<InventoryReceiveItemViewModel> ReceiveItems { get; set; }
    }
}