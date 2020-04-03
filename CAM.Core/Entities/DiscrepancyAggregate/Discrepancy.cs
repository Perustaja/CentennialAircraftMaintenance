using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAM.Core.Entities.DiscrepancyAggregate
{
    /// <summary>
    /// Contains information used for maintenance documents and tracking purposes. 
    /// </summary>
    public class Discrepancy
    {
        public int Id { get; private set; }

        public string AircraftId { get; private set; }

        public int WorkStatusId { get; private set; }

        public int WorkOrderId { get; private set; }
        // Main
        [StringLength(15)]
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Resolution { get; private set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; private set; }
        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        public DateTime? DateFinalized { get; private set; }
        [Display(Name = "Created by")]
        [StringLength(60)]
        public string CreatedBy { get; private set; }
        // Navigation properties
        public Aircraft Aircraft { get; private set; }
        public List<LaborRecord> LaborRecords { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
        public List<DiscrepancyPart> DiscrepancyParts { get; private set; }
        private Discrepancy()
        {
            // Required by EF
        }
        public Discrepancy(string aircraftId, int workOrderId, int workStatusId, string title,
        string description, string createdBy)
        {
            AircraftId = aircraftId;
            WorkOrderId = workOrderId;
            WorkStatusId = workStatusId;
            Title = title;
            Description = description;
            CreatedBy = createdBy;
            DateCreated = DateTime.Today;
        }
    }
}