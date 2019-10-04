using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Used to organize Discrepancies, acting like a container. 
    /// </summary>
    public class WorkOrderDto
    {
        public int Id { get; set; }
        // Main
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        // Navigation properties
        public ICollection<DiscrepancyDto> Discrepancies { get; set; }
    }
}