namespace Smart_Garage.Models
{
    public class MechanicVisit
    {
        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
    }
}
