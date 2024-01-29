using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IUsersService
    {
        IList<User> GetAll();
        UserResponseDTO GetById(int id);
        UserResponseDTO GetByName(string username);
        UserResponseDTO Create(SignUpUserRequestDTO newUser); // Sign Up
        UserResponseDTO Update(int id, UpdateUserRequestDTO updatedUser);
        User Delete(int id, string username);
        string Login(LoginUserRequestDTO user);
        IList<User> FilterBy(UserQueryParameters usersParams);
        int GetCount();
        bool UserExists(string username);
        bool IsCurrentUserAdmin(string currentUser); // "currentUser" is username
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
