﻿using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IUserRepository
    {
        User Create(User newUser); // Register
        IList<User> GetAll();
        IList<User> GetAllNotAdmins();
        User GetById(int id);
        User GetByName(string name);
        User Update(int id, User updatedUser);
        User SetPassword(string email, User updatedUser);
		User Delete(int id);
        bool UserExists(string username);
        bool EmailExists(string email);
        bool PhoneNumberExists(string phoneNumber);
        int Count();
        IList<User> FilterBy(UserQueryParameters usersParams);
    }
}
