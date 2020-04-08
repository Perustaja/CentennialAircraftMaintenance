using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains information that a Discrepancy may use to build itself. Created by users.
    /// </summary>
    public class DiscrepancyTemplate
    {
        public int Id { get; private set; }
        public int? WorkOrderTemplateId { get; private set; }
        [StringLength(15)]
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Resolution { get; private set; }
        // Navigation Properties
        public WorkOrderTemplate WorkOrderTemplate { get; private set; }
        public List<DiscrepancyTemplatePart> DiscrepancyTemplateParts { get; private set; }
        // Required by EF for join table creation, will not be accessed
        public List<WorkOrderTemplateDiscrepancyTemplate> WorkOrderTemplateDiscrepancyTemplates { get; private set; }
        private DiscrepancyTemplate()
        {
            // Required by EF
        }
        public DiscrepancyTemplate(int? workOrderTemplateId, string title, string description,
        string resolution)
        {
            WorkOrderTemplateId = workOrderTemplateId;
            Title = title;
            Description = description;
            resolution = Resolution;
        }
        public void ChangeTitle(string title) => Title = title;
        public void ChangeDescription(string desc) => Description = desc;
        public void ChangeResolution(string reso) => Resolution = reso;
    }
}