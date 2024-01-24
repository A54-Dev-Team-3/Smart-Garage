using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1,2}\s?\d{4}[A-Z]{2}$")]
        public string LicensePlate { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        public string VIN { get; set; }

        [Required]
        [Range(1886, 2050)]
        public int CreationYear { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Model { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Brand { get; set; }

        public User User { get; set; }

        public IList<Service> Services { get; set; } = new List<Service>();
    }
}
