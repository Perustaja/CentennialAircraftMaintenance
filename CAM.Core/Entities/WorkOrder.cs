using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Used to organize Discrepancies, acting like a container. 
    /// </summary>
    public class WorkOrder : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Main
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Finalized")]
        [DataType(DataType.Date)]
        public DateTime DateFinalized { get; set; }
        // Navigation properties
        public List<Discrepancy> Discrepancies { get; set; }
    }
}