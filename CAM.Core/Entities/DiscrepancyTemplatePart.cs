namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents the join table for DiscrepancyTemplate and Part. A Discrepancy will build its own
    /// join record from this.
    /// </summary>
    public class DiscrepancyTemplatePart
    {
        public int DiscrepancyTemplateId { get; private set; }
        public string PartId { get; private set; }
        public int Qty { get; private set; }
        public DiscrepancyTemplate DiscrepancyTemplate { get; private set; }
        public Part Part { get; private set; }
        private DiscrepancyTemplatePart()
        {
            // Required by EF  
        }
        public DiscrepancyTemplatePart(int discrepTempId, string partId, int qty)
        {
            DiscrepancyTemplateId = discrepTempId;
            PartId = partId;
            Qty = qty;
        }
        public void AddQuantity(int qty) => Qty += qty;
        public void UpdateQuantity(int qty) => Qty = qty;
    }
}