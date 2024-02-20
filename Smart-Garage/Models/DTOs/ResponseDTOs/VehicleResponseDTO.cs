using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class VehicleResponseDTO
    {
        public int Id { get; set; }
        [Required]
		[DisplayName("License Plate")]
		public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public int CreationYear { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public UserResponseDTO User { get; set; }
        public List<Visit> Visits { get; set; }
    }
}
