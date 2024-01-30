using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class CreateServiceResponseDTO
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int VehicleId { get; set; }
    }
}
