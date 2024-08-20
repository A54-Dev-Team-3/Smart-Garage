using System.ComponentModel.DataAnnotations;
using Smart_Garage.Models.DTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

namespace Smart_Garage.Models.ViewModel
{
    public class CreateVehicleViewModel
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
        public IList<BrandViewModel> Brands { get; set; }
        public int BrandId { get; set; }
        //public IList<Visit> Visits { get; set; }
    }
}
