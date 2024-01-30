using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class CreateServiceRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        public int VehicleId { get; set; }
    }
}
