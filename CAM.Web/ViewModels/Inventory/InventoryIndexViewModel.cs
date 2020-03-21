using CAM.Core.SharedKernel;
using CAM.Web.ViewModels.Shared;

namespace CAM.Web.ViewModels.Inventory
{
    public class InventoryIndexViewModel
    {
        public PaginatedList<PartViewModel> PaginatedParts { get; set; }
    }
}