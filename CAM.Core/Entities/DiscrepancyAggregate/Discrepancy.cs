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
        private Discrepancy()
        {
            // Required by EF
        }
        public Discrepancy(string aircraftId, int? workOrderId, int workStatusId, int? squawkId,
            string title, string description, string createdBy)
        {
            AircraftId = aircraftId;
            WorkOrderId = workOrderId;
            WorkStatusId = workStatusId;
            SquawkId = squawkId;
            Title = title;
            Description = description;
            CreatedBy = createdBy;
            DateCreated = DateTime.Today;
        }
        public int Id { get; set; }

        public string AircraftId { get; private set; }

        public int WorkStatusId { get; private set; }

        public int? WorkOrderId { get; private set; }

        public int? SquawkId { get; private set; }

        // Main
        [StringLength(15)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }

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
        public Aircraft Aircraft { get; set; }
        public List<LaborRecord> LaborRecords { get; set; }
        public WorkStatus WorkStatus { get; set; }
        public List<DiscrepancyPart> DiscrepancyParts { get; set; }
    }
}