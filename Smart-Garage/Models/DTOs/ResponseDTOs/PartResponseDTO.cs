using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class PartResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
