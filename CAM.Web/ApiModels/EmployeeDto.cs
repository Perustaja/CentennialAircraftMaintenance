using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class EmployeeDto
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