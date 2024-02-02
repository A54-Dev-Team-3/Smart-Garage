using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Mechanic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public IList<ServiceInstanceMechanic> ServiceInstanceMechanic { get; set; } = new List<ServiceInstanceMechanic>();
    }
}
