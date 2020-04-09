using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents the join table for Discrepancy and Part
    /// </summary>
    public class DiscrepancyPart
    {
        public int DiscrepancyId { get; private set; }
        public int PartId { get; private set; }
        [Range(0, 99)]
        public int Qty { get; private set; }
        public Discrepancy Discrepancy { get; private set; }
        public Part Part { get; private set; }
        private DiscrepancyPart()
        {
            // Required by EF  
        }
        public DiscrepancyPart(int discrepId, int partId, int qty)
        {
            DiscrepancyId = discrepId;
            PartId = partId;
            Qty = qty;
        }
        public void AddQuantity(int qty) => Qty += qty;
        public void UpdateQuantity(int qty) => Qty = qty;
    }
}