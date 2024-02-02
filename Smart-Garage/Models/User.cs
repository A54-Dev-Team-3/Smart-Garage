using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdmin { get; set; }
        public IList<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
    }
}
