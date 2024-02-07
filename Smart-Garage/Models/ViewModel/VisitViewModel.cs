using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class VisitViewModel
    {
        public int Id { get; set; }

        public Vehicle Vehicle { get; set; }

        public DateTime Date { get; set; }
        public int TotalPrice { get; set; } // in BGN
        public int ServicesTotalPrice { get; set; } // in BGN
        public int PartsTotalPrice { get; set; } // in BGN
    }
}
