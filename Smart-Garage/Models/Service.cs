using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool IsDeleted { get; set; }
    }
}
