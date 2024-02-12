using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class PartViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
