namespace Smart_Garage.Models
{
    public class VehicleService
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
