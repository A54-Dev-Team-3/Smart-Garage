using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class UpdateServiceResponseDTO
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int VehicleId { get; set; }
    }
}
