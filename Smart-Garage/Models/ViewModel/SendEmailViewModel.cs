using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
    public class SendEmailViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
