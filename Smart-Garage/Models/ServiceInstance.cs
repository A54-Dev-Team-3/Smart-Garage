using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class ServiceInstance
    {
        [Key]
        public int Id { get; set; }
        

        [Required]
        [Range(0, int.MaxValue)]
        public int PartQuantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]

        public double PartUnitPrice { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public double ServicePrice { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        

        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }


        public bool IsDeleted { get; set; }
    }
}
