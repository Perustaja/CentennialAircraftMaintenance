using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities;
using CAM.Core.Enums;
using CAM.Web.ViewModels.Shared;

namespace CAM.Web.ViewModels.Discrepancies
{
    [AutoMap(typeof(Discrepancy), ReverseMap = true)]
    public class DiscrepanciesDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Aircraft: ")]
        public string AircraftId { get; set; }
        [Display(Name = "Work Order #: ")]
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
        [Display(Name = "Created by")]
        [StringLength(20)]
        public string CreatedBy { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
        [SourceMember(nameof(Discrepancy.DiscrepancyParts))]
        public List<DiscrepancyPartViewModel> DiscrepancyPartViewModels { get; set; }
        [SourceMember(nameof(Discrepancy.LaborRecords))]
        public List<LaborRecordViewModel> LaborRecordViewModels { get; set; }
        public string SinceCreationMsg => $"Created {(DateTime.Today - DateCreated).Days} days ago by {CreatedBy}";
    }
}