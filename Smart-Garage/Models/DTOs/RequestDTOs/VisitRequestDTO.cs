using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Models.DTOs.RequestDTOs
{
    public class VisitRequestDTO
    {
        public DateTime Date { get; set; }
        public double PartsTotalPrice { get; set; } // in BGN
        public double ServicesTotalPrice { get; set; } // in BGN
        public double TotalPrice { get; set; } // in BGN
        public int VehicleId { get; set; }
        public IList<ServiceInstanceRequestDTO> ServiceInstances { get; set; }
    }
}
