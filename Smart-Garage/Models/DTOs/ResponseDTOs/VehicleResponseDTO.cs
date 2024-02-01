using System.Globalization;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class VehicleResponseDTO
    {
        public string LicencePlate { get; set; }
        public string VIN { get; set; }
        public int CreationYear { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string User { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
    }
}
