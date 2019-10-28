namespace CAM.Core.Options
{
    /// <summary>
    /// Options for FspScraper with user/pass stored in usersecrets(development).
    /// </summary>
    public class FspScraperOptions
    {
        public string LoginUrl = "https://app.flightschedulepro.com/Account/Login";
        public string AircraftUrl = "https://app.flightschedulepro.com/App/Aircraft/";
        public string FspUser { get; set; }
        public string FspPass { get; set; }
    }
}
