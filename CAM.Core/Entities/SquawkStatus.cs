using System.Collections.Generic;
using CAM.Core.SharedKernel;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data used to represent the current status of a squawk.
    /// </summary>
    public class SquawkStatus : BaseEntity<int>
    {
        public override int Id { get; set; }
        // Main
        public string Name { get; set; }
        public bool IsOpen { get; set; } = true;
    }
}