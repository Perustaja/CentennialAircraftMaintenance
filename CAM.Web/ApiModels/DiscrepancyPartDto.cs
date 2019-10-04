using CAM.Core.Entities;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Represents the join table for Discrepancy and Part
    /// </summary>
    public class DiscrepancyPartDto
    {
        public int DiscrepancyId { get; set; }
        public int PartId { get; set; }
        // Discrepancy
        public Discrepancy Discrepancy { get; set; }
        // Part
        public Part Part { get; set; }

    }
}