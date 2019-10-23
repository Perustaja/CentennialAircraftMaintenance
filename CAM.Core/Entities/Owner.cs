using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Aircraft owner, contains personal information including email.
    /// </summary>
    public class Owner
    {
        public int Id { get; set; }
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