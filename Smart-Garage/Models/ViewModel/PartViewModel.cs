using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class PartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
