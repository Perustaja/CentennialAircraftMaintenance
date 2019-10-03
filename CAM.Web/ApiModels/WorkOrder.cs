using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Used to organize Discrepancies, acting like a container. 
    /// </summary>
    public class WorkOrder
    {
        public int Id { get; set; }
        // Main
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        // Navigation properties
        public ICollection<Discrepancy> Discrepancies { get; set; }
    }
}