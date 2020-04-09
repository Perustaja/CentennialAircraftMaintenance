using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class Employee
    {
        public int Id { get; private set; }

        // Main
        [Required]
        [StringLength(50)]
        public string FirstName { get; private set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; private set; }

        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();

        [StringLength(20)]
        public string CertificationNum { get; private set; }
        private Employee()
        {
            // Required by EF
        }
        public Employee(string firstName, string lastName, string certNum)
        {
            FirstName = firstName;
            LastName = lastName;
            CertificationNum = certNum ?? "N/A";
        }
        public void EditEmployee(string firstName, string lastName, string certNum)
        {
            FirstName = firstName;
            LastName = lastName;
            UpdateCertificationNumber(certNum);
        }
        public void UpdateCertificationNumber(string certNum) => CertificationNum = certNum ?? "N/A";
    }
}