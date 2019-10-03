using System.Collections.Generic;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data used to represent the current status of a squawk.
    /// </summary>
    public class Status
    {

        public int Id { get; set; }
        // Main
        public string Name { get; set; }
        public bool IsOpen { get; set; } = true;
    }
}