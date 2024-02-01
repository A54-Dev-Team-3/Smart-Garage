using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;

namespace Smart_Garage.Services.Contracts
{
    public interface IUsersService
    {
        UserResponseDTO Create(SignUpUserRequestDTO newUser); // Sign Up
        IList<UserResponseDTO> GetAll();
        IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters, string username);
        UserResponseDTO GetById(int id);
        UserResponseDTO GetByName(string username);
        UserResponseDTO Update(int id, UpdateUserRequestDTO updatedUser, string username);
        User Delete(int id, string username);
        string Login(LoginUserRequestDTO user);
        int GetCount();
        bool UserExists(string username);
        bool IsCurrentUserAdmin(string currentUser); // "currentUser" is username
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
