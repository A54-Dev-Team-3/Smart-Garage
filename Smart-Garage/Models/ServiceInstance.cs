using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class ServiceInstance
    {
        [Key]
        public int Id { get; set; }
        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public ICollection<ServiceInstanceService> ServiceInstanceServices { get; set; } = new HashSet<ServiceInstanceService>();
        public ICollection<ServiceInstancePart>? ServiceInstanceParts { get; set; } = new HashSet<ServiceInstancePart>();
    }
}
