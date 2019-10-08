using System.ComponentModel.DataAnnotations;

namespace CAM.Core.SharedKernel
{
    /// <summary>
    /// Defines the various meters and other times to be tracked
    /// </summary>
    public abstract class TimesHolder
    {
        public decimal Hobbs { get; set; }
        [Display(Name = "Air Time")]
        public int AirTime { get; set; }
        [Display(Name = "Tach 1")]
        public decimal Tach1 { get; set; }
        [Display(Name = "Tach 2")]
        public decimal Tach2 { get; set; }
        [Display(Name = "Prop 1")]
        public decimal Prop1 { get; set; }
        [Display(Name = "Prop 2")]
        public decimal Prop2 { get; set; }
        [Display(Name = "Aircraft Total")]
        public decimal AircraftTotal { get; set; }
        [Display(Name = "Engine 1 Total")]
        public decimal Engine1Total { get; set; }
        [Display(Name = "Engine 2 Total")]
        public decimal Engine2Total { get; set; }
        public int Cycles { get; set; }
    }
}