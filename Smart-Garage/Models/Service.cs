using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Service
    {
        public string Labour { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }
    }
}
