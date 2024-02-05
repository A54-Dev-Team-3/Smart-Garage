using System.ComponentModel.DataAnnotations;

namespace Smart_Garage.Models.ViewModel
{
	public class SignUpViewModel
	{
		[Required]
		[MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
		[MaxLength(20, ErrorMessage = "The {0} must be no more than {1} characters long.")]
		public string Username { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[+*^$%&!@#_-])[A-Za-z\d+*^$%&!@#_-]{8,}$")]
		public string Password { get; set; }

		[Required]
		[RegularExpression(@"^\d{10}$")]
		public string PhoneNumber { get; set; }
	}
}
