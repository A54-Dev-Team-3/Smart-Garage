using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class ServiceInstance
    {
        [Key]
        public int Id { get; set; }

        public IList<ServiceInstanceMechanic> ServiceInstanceMechanics { get; set; } = new List<ServiceInstanceMechanic>();
        public IList<ServiceInstanceService> ServiceInstanceServices { get; set; } = new List<ServiceInstanceService>();
        public IList<ServiceInstancePart>? ServiceInstanceParts { get; set; } = new List<ServiceInstancePart>();
    }
}
