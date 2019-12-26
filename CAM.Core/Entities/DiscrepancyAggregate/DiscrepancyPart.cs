using CAM.Core.SharedKernel;

namespace CAM.Core.Entities.DiscrepancyAggregate
{
    /// <summary>
    /// Represents the join table for Discrepancy and Part
    /// </summary>
    public class DiscrepancyPart
    {
        public int DiscrepancyId { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }
        // Required by EF
        public Discrepancy Discrepancy { get; set; }
        public Part Part { get; set; }
    }
}