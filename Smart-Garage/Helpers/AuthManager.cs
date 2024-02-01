using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Helpers
{
    public class AuthManager
    {
        private const string InvalidCredentialsErrorMessage = "Invalid credentials!";
        private readonly IUsersService userService;

        public AuthManager(IUsersService userService)
        {
            this.userService = userService;
        }

        public virtual User AuthenticateUser(string username, string password)
        {

            try
            {
                var user = userService.Authenticate(username);

                if (!userService.VerifyPasswordHash(password,
                user.PasswordHash, user.PasswordSalt))
                {
                    throw new WrongPasswordException(InvalidCredentialsErrorMessage);
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException(InvalidCredentialsErrorMessage);
            }
        }
    }
}
