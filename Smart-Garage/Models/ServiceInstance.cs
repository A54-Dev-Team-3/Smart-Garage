using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class ServiceInstance
    {
        [Key]
        public int Id { get; set; }

        public ICollection<ServiceInstanceMechanic> ServiceInstanceMechanics { get; set; } = new HashSet<ServiceInstanceMechanic>();
        public ICollection<ServiceInstanceService> ServiceInstanceServices { get; set; } = new HashSet<ServiceInstanceService>();
        public ICollection<ServiceInstancePart>? ServiceInstanceParts { get; set; } = new HashSet<ServiceInstancePart>();
    }
}
