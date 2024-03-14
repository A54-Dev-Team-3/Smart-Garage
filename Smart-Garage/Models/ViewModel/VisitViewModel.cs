using Smart_Garage.Models.DTOs.ResponseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PartsTotalPrice { get; set; } // in BGN
        public double ServicesTotalPrice { get; set; } // in BGN
        public double TotalPrice { get; set; } // in BGN
        public VehicleVisitViewModel Vehicle { get; set; }
        public CustomerVisitViewModel User { get; set; }
        public IList<PartViewModel> Parts { get; set; }
        public IList<ServiceViewModel> Services { get; set; }
        public IList<MechanicViewModel> Mechanics { get; set; }
        public IList<BrandViewModel> Brands { get; set; }
    }
}
