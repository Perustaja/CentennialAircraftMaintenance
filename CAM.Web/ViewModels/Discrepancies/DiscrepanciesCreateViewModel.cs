using System;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Discrepancies
{
    public class DiscrepanciesCreateViewModel
    {
        public int Id { get; set; }

        public int? WorkOrderId { get; set; }

        [StringLength(15)]
        public string Title { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Created by")]
        [StringLength(60)]
        public string CreatedBy { get; set; }

        [Required]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Resolution { get; set; }
    }
}