namespace Smart_Garage.Models
{
    public class VehicleBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<VehicleModel> Models { get; set; } = new HashSet<VehicleModel>();
    }
}
