using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Aircraft owner, contains personal information including email.
    /// </summary>
    public class Owner : BaseEntity<int>
    {
        public override int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        // Navigation Properties
        public ICollection<AircraftOwner> AircraftOwners { get; set; }
    }
}