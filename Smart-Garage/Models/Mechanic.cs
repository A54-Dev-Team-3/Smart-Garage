using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Mechanic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<MechanicVisit> MechanicVisits { get; set; } = new HashSet<MechanicVisit>();
    }
}
