using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data used to represent the current status of a workorder or discrepancy.
    /// </summary>
    public class WorkStatus : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Main
        [StringLength(15)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}