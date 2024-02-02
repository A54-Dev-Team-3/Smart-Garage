using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class ServiceInstanceService
    {
        public int ServiceInstanceId { get; set; }
        public ServiceInstance ServiceInstance { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
