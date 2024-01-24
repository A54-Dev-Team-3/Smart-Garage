using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Labour { get; set; }
        [Required]

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
