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

        public ICollection<ServiceInstanceService> ServiceInstanceServices { get; set; } = new HashSet<ServiceInstanceService>();

    }
}
