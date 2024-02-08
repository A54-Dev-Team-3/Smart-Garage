using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class VehicleResponseDTO
    {
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public int CreationYear { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int UserId { get; set; }
        public IList<Visit> Visits { get; set; }
    }
}
