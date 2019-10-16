using System.Collections.Generic;
using CAM.Core.SharedKernel;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains data used to represent the current status of a squawk.
    /// </summary>
    public class StatusDto
    {
        public int Id { get; set; }
        // Main
        public string Name { get; set; }
        public bool IsOpen { get; set; } = true;
    }
}