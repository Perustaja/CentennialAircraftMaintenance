using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CAM.Core.Entities
{
    public class WorkOrderTemplate
    {
        public int Id { get; private set; }
        [Required]
        [StringLength(40)]
        public string Title { get; private set; }
        // Navigation properties
        public List<WorkOrderTemplateDiscrepancyTemplate> WorkOrderTemplateDiscrepancyTemplates { get; private set; }
        private WorkOrderTemplate()
        {
            // Required by EF
        }
        public WorkOrderTemplate(string title)
        {
            ChangeTitle(title);
        }
        public void ChangeTitle(string title) => Title = title ?? "Unset title";
    }
}