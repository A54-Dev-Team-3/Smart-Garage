using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
