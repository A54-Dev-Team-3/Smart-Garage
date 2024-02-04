using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class PartRequestDTO
    {
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
    }
}
