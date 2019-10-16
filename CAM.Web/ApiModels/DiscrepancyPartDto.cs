using CAM.Core.SharedKernel;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Represents the join table for Discrepancy and Part
    /// </summary>
    public class DiscrepancyPartDto
    {
        public int DiscrepancyId { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }
    }
}