using System.Collections.Generic;

namespace CAM.Core
{
    public class WorkOrderTemplate
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public List<DiscrepancyTemplate> DiscrepancyTemplates { get; private set; }
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