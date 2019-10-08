using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        // Main
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(20)]
        public string CertificationNum { get; set; }
    }
}