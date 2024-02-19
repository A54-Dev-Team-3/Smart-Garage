using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class ServiceInstanceRequestDTO
    {
        public int PartQuantity { get; set; }
        public double PartUnitPrice { get; set; }
        public PartRequestDTO Part { get; set; }

        public double ServicePrice { get; set; }
        public ServiceRequestDTO Service { get; set; }

        public MechanicRequestDTO Mechanic { get; set; }
    }
}
