﻿using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Services.Contracts
{
    public interface IUserService
    {
        UserResponseDTO Create(SignUpUserRequestDTO newUser); // Sign Up
        IList<UserResponseDTO> GetAll();
        IList<UserResponseDTO> GetAllNotAdmins();
        IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters);
        UserResponseDTO GetById(int id);
        UserResponseDTO GetByName(string username);
        User Authenticate(string username);
        UserResponseDTO Update(int id, UpdateUserRequestDTO updatedUser, string username);
        void SetPassword(string email, string password);
		User Delete(int id, string username);
        string Login(LoginUserRequestDTO user);
        int GetCount();
        bool UserExists(string username);
        bool EmailExists(string email);
        void SendEmailLogic(SendEmailViewModel userEmail, string message);
        void IsCurrentUserAdmin(string currentUser); // "currentUser" is username
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
