using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IUsersService
    {
        IList<User> GetAll();
        User GetUser(string username);
        User GetUser(int id);
        User Create(UserRequestDTO newUser); // Sign Up
        User Update(int id, User user);
        User Delete(int id, string username);
        string Login(UserRequestDTO user);
        IList<User> FilterBy(UserQueryParameters usersParams);
        int GetCount();
        bool UserExists(string username);
        bool IsCurrentUserAdmin(string currentUser); // "currentUser" is username
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
