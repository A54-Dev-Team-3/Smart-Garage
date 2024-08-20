namespace Smart_Garage.Models
{
    public class PartVisit
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
    }
}
