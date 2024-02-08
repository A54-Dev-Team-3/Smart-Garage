using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }

        public bool IsDeleted { get; set; }

        // Image ?
        public ICollection<ServiceInstancePart> ServiceInstanceParts { get; set; } = new HashSet<ServiceInstancePart>();
    }
}
