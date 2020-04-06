using System;
using System.Collections.Generic;
using System.Linq;

namespace CAM.Core.Entities
{
    public class WorkOrderTemplate
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        // Navigation properties
        public List<WorkOrderTemplateDiscrepancyTemplate> WorkOrderTemplateDiscrepancyTemplates { get; private set; }
        private WorkOrderTemplate()
        {
            // Required by EF
        }
        public WorkOrderTemplate(string title)
        {
            Title = title;
        }
        public void ChangeTitle(string title) => Title = title;
    }
}