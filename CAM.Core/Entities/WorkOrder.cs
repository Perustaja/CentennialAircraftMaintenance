using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CAM.Core.Enums;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Used to organize Discrepancies, acting like a container. 
    /// </summary>
    public class WorkOrder
    {
        public int Id { get; private set; }
        // Main
        [Required]
        [StringLength(20)]
        [Display(Name = "Registration")]
        public string AircraftId { get; private set; }
        [Required]
        [StringLength(40)]
        public string Title { get; private set; }
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
        // Navigation Properties
        public List<Discrepancy> Discrepancies { get; private set; }
        private WorkOrder()
        {
            // Required by EF
        }
        public WorkOrder(string aircraftId, string title, string createdBy)
        {
            AircraftId = aircraftId;
            Title = title;
            DateCreated = DateTime.Today;
            CreatedBy = createdBy;
            WorkStatus = WorkStatus.Open;
        }
        public void ChangeTitle(string title) => Title = title;
        public void SubmitForReview()
        {
            WorkStatus = WorkStatus.UnderReview;
        }
        public void ApproveAfterReview()
        {
            WorkStatus = WorkStatus.Approved;
        }
    }
}