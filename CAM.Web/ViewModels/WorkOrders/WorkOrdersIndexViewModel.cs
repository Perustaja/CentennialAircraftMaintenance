using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace CAM.Web.ViewModels.WorkOrders
{
    [AutoMap(typeof(WorkOrder), ReverseMap = true)]
    public class WorkOrdersIndexViewModel
    {
        public string Title  { get; set; }

        [Display(Name = "Registration")]
        public string AircraftId { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        public DateTime? DateFinalized { get; set; }

        public List<Discrepancy> Discrepancies { get; set; }

        [SourceMember(nameof(WorkOrder.WorkStatus))]
        public WorkStatusViewModel WorkStatusViewModel { get; set; }
    }
}