using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains aircraft time information. AircraftId is NOT a FK of Aircraft. The naming seems proper and is kept for ease of use when programming.
    /// However, Aircraft.Id is a FK based on Times.AircraftId. This is done to prevent errors if a plane is added quickly and 
    /// the times scraper tries to add a Times row for a nonexistant Aircraft.
    /// </summary>
    public class Times
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string AircraftId { get; set; }
        [Display(Name = "Hobbs Time")]
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
        // Aircraft
        public Aircraft Aircraft { get; set; }
    }
}