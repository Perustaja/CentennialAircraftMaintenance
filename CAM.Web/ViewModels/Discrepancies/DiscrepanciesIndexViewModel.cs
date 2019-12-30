using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAM.Core.Entities.DiscrepancyAggregate;

namespace CAM.Web.ViewModels.Discrepancies
{
    [AutoMap(typeof(Discrepancy), ReverseMap = true)]
    public class DiscrepanciesIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true ,DataFormatString = Constants.DATE_FORMAT)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true ,DataFormatString = Constants.DATE_FORMAT)]
        public DateTime DateFinalized { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Registration")]
        public string AircraftId { get; set; }

        [SourceMember(nameof(Discrepancy.WorkStatus))]
        public WorkStatusViewModel WorkStatusViewModel { get; set; }
    }
}