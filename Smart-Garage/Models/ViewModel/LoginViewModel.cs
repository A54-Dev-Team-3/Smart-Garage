using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class LogInViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
