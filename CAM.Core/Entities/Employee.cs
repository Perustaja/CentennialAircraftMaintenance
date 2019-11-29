using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class Employee : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Main
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(2)]
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();
        
        [StringLength(20)]
        public string CertificationNum { get; set; }
    }
}