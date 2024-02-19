using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class CreateServiceViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
    }
}
