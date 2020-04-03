using System;
using System.Collections.Generic;

namespace CAM.Core
{
    public class DiscrepancyTemplate
    {
        public int Id { get; private set; }
        public int? WorkOrderTemplateId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Resolution { get; private set; }
        public List<string> PartIds { get; private set; }
        public List<int> PartQtys { get; private set; }
        // Navigation Property
        public WorkOrderTemplate WorkOrderTemplate { get; private set; }
        private DiscrepancyTemplate()
        {
            // Required by EF
        }
        public DiscrepancyTemplate(int? workOrderTemplateId, string title, string description, 
        string resolution, List<string> partIds, List<int> partQtys)
        {
            WorkOrderTemplateId = workOrderTemplateId;
            Title = title;
            Description = description;
            resolution = Resolution;
            PartIds = partIds;
            PartQtys = partQtys;
        }
        public void ChangeTitle(string title) => Title = title;
        public void ChangeDescription(string desc) => Description = desc;
        public void ChangeResolution(string reso) => Resolution = reso;
        public void AddPart(string id, int qty)
        {
            if (!String.IsNullOrWhiteSpace(id) && qty > 0)
            {
                PartIds.Add(id);
                PartQtys.Add(qty);
            }
            else
                throw new ArgumentException($"Discrepancy Template {Id}: Attempted to add a null part id or qty less than 1");
        }
        public void RemovePart(int index)
        {
            // This may need to change to remove based off of string value using IndexOf for qty
            if (index >= 0 && PartIds.Count > index)
            {
                PartIds.RemoveAt(index);
                PartQtys.RemoveAt(index);
            }
            else
                throw new ArgumentOutOfRangeException($"Discrepancy Template {Id}: Attempted to remove index {index} from a list with count {PartIds.Count}");
        }

    }
}