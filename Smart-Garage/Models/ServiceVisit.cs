namespace Smart_Garage.Models
{
    public class ServiceVisit
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
    }
}
