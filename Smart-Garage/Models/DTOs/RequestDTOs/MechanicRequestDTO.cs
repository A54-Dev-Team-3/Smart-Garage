using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class MechanicRequestDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
