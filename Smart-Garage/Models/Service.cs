using Smart_Garage.Services;
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

        public ICollection<ServiceVisit> ServiceVisits { get; set; } = new HashSet<ServiceVisit>();

    }
}
