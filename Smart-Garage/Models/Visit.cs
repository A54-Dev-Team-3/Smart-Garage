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
        public double TotalPrice { get; set; } // in BGN
        public double ServicesTotalPrice { get; set; } // in BGN
        public double PartsTotalPrice { get; set; } // in BGN
        public bool IsDeleted { get; set; }
        public ICollection<ServiceInstance> ServiceInstances { get; set; } = new HashSet<ServiceInstance>();
    }
}
