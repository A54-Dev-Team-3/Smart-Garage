using Smart_Garage.Models.DTOs.ResponseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class VisitViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public double TotalPrice { get; set; } // in BGN
        public double ServicesTotalPrice { get; set; } // in BGN
        public double PartsTotalPrice { get; set; } // in BGN
        public VehicleViewModel Vehicle { get; set; }
        public IList<ServiceInstanceViewModel> ServiceInstances { get; set; }
    }
}
