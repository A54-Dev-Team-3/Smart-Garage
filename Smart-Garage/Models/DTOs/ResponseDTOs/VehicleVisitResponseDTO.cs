namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class VehicleVisitResponseDTO
    {
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public int CreationYear { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}
