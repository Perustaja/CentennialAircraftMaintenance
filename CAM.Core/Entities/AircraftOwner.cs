namespace CAM.Core.Entities
{
    /// <summary>
    /// Represents the join table for Aircraft and Owner
    /// </summary>
    public class AircraftOwner
    {
        public string AircraftId { get; set; }
        public int OwnerId { get; set; }
        // Aircraft
        public Aircraft Aircraft { get; set; }
        // Owner
        public Owner Owner { get; set; }
    }
}