using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.DTOs.ResponseDTOs
{
    public class VisitResponseDTO
    {
        public int Id { get; set; }

        public VehicleResponseDTO Vehicle { get; set; }

        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public double ServicesTotalPrice { get; set; }
        public double PartsTotalPrice { get; set; }
        public ICollection<ServiceInstance> ServiceInstances { get; set; } = new HashSet<ServiceInstance>();
    }
}
