using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ViewModels.Shared
{
    public class WorkStatusListViewModel
    {
        [StringLength(15)]
        [Display(Name = "Status")]
        public List<string> Descriptions { get ; set; }
        public List<bool> Selected { get; set; }
    }
}