namespace Smart_Garage.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public VehicleBrand Brand { get; set; }
        public bool IsDeleted { get; set; }
    }
}
