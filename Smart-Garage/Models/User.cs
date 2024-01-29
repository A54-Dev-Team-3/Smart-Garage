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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        public IList<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
        public bool IsAdmin { get; set; }
    }
}
