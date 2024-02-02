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
        public double Price { get; set; }

        public bool IsDeleted { get; set; }

        public IList<ServiceInstanceService> ServiceInstanceServices { get; set; } = new List<ServiceInstanceService>();

    }
}
