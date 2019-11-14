using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Shared
{
    public class AircraftListViewModel
    {
        [StringLength(20)]
        [Display(Name = "Registration")]
        public List<string> Ids { get ; set; }
    }
}