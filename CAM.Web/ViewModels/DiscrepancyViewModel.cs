using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Entities.DiscrepancyAggregate;

namespace CAM.Web.ViewModels
{
    [AutoMap(typeof(Discrepancy), ReverseMap = true)]
    public class DiscrepancyViewModel
    {
        public int Id { get; set; }

        // Main
        [StringLength(15)]
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
        [StringLength(60)]
        public string CreatedBy { get; set; }

        // Aircraft properties
        [StringLength(20)]
        [Display(Name = "Registration")]
        public string AircraftId { get; set; }
        // WorkStatus
        public WorkStatusViewModel WorkStatusViewModel { get; set; }
    }
}