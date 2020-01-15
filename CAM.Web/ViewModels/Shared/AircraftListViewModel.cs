using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Shared
{
    public class AircraftListViewModel
    {
        [Display(Name = "Registration")]
        public List<string> Ids { get ; set; }
        public List<bool> Selected { get; set; }
    }
}