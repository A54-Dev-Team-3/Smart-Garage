using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<PartVisit> PartVisits { get; set; } = new HashSet<PartVisit>();
        public ICollection<ServiceVisit> ServiceVisits { get; set; } = new HashSet<ServiceVisit>();
        public ICollection<MechanicVisit> MechanicVisits { get; set; } = new HashSet<MechanicVisit>();
    }
}
