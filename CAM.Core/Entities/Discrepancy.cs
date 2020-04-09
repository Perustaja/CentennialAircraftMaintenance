using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.Enums;
using System.Linq;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains information used for maintenance documents and tracking purposes. 
    /// </summary>
    public class Discrepancy
    {
        public int Id { get; private set; }

        public string AircraftId { get; private set; }

        public int WorkOrderId { get; private set; }
        // Main
        [StringLength(40)]
        public string Title { get; private set; }
        [Required]
        [StringLength(75)]
             
        public string Description { get; private set; }
        [StringLength(600)]
        public string Resolution { get; private set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; private set; }
        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        public DateTime? DateFinalized { get; private set; }
        [Display(Name = "Created by")]
        [StringLength(20)]
        public string CreatedBy { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
        // Navigation properties
        public Aircraft Aircraft { get; private set; }
        public List<LaborRecord> LaborRecords { get; private set; }
        public List<DiscrepancyPart> DiscrepancyParts { get; private set; }
        private Discrepancy()
        {
            // Required by EF
        }
        public Discrepancy(string aircraftId, int workOrderId, string title, string description,
        string createdBy)
        {
            AircraftId = aircraftId;
            WorkOrderId = workOrderId;
            Title = title;
            Description = description;
            DateCreated = DateTime.Today;
            CreatedBy = createdBy;
            WorkStatus = WorkStatus.Open;
        }
        public void ChangeTitle(string title) => Title = title;
        public void ChangeDescription(string desc) => Description = desc;
        public void ChangeResolution(string reso) => Resolution = reso;
        public void SubmitForReview()
        {
            WorkStatus = WorkStatus.UnderReview;
        }
        public void ApproveAfterReview()
        {
            WorkStatus = WorkStatus.Approved;
            DateFinalized = DateTime.Today;
        }
    }
}