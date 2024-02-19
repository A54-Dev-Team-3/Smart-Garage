using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
