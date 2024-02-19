using Smart_Garage.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class CreateVehicleRequestDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"^[A-C, E, H, K, M, O, P, T, X, Y]{1,2}\s?\d{4}[A-C, E, H, K, M, O, P, T, X, Y]{2}$")]
        public string LicensePlate { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        public string VIN { get; set; }

        [Required]
        [Range(1886, 2024)]
        public int CreationYear { get; set; }
        public CustomerViewModel? User { get; set; }
        public IList<Visit> Visits { get; set; }
    }
}

