namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents the join table between a WorkOrderTemplate and a DiscrepancyTemplate
    /// This allows resuse of DiscrepancyTemplates, e.g. multiple WorkOrderTemplates may include
    /// a DiscrepancyTemplate that involves removing an engine, taking a component off, etc.
    /// </summary>
    public class WorkOrderTemplateDiscrepancyTemplate
    {
        public int WorkOrderTemplateId { get; private set; }
        public int DiscrepancyTemplateId { get; private set; }
        public WorkOrderTemplate WorkOrderTemplate { get; private set; }
        public DiscrepancyTemplate DiscrepancyTemplate { get; private set; }
        private WorkOrderTemplateDiscrepancyTemplate()
        {
            // Required by EF
        }
        public WorkOrderTemplateDiscrepancyTemplate(int wOrderTempId, int dTempId)
        {
            WorkOrderTemplateId = wOrderTempId;
            DiscrepancyTemplateId = dTempId;
        }
    }
}