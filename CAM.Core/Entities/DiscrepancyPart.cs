using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents the join table for Discrepancy and Part
    /// </summary>
    public class DiscrepancyPart : BaseEntity
    {
        public int DiscrepancyId { get; set; }
        public int PartId { get; set; }
        // Discrepancy
        public Discrepancy Discrepancy { get; set; }
        // Part
        public Part Part { get; set; }

    }
}