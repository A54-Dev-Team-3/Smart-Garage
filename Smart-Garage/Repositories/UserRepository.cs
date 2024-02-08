using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;
using System.Globalization;

namespace Smart_Garage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SGContext context;
        private readonly IMapper autoMapper;

        public UserRepository(SGContext context, IMapper autoMapper)
        {
            this.context = context;
            this.autoMapper = autoMapper;
        }

        public IList<User> GetAll()
        {
            return context.Users.Where(u=> !u.IsDeleted).ToList();
        }

        public IList<User> GetAllNotAdmins()
        {
            return context.Users.Where(u => !u.IsAdmin && !u.IsDeleted).ToList();
        }

        public User GetById(int id)
        {
            User user = context.Users
                .Include(u => u.Vehicles)
                .FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");

            return user;
        }


        public User GetByName(string name)
        {
            return context.Users.FirstOrDefault(u => u.Username == name && !u.IsDeleted) ??
               throw new EntityNotFoundException($"User with username:\"{name}\" is not found.");
        }

        public User Create(User newUser)
        {
            var username = newUser.Username;
            var email = newUser.Email;
            var phoneNumber = newUser.PhoneNumber;

            if (UserExists(username))
                throw new DuplicationException($"Username \"{username}\" already exists!");

            if (EmailExists(email))
                throw new DuplicationException($"Email \"{email}\" already exists!");

            if (PhoneNumberExists(phoneNumber))
                throw new DuplicationException($"PhoneNumber \"{phoneNumber}\" already exists!");

            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser;
        }

        public User Update(int id, User updatedUser)
        {
            User newUser = context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");

            newUser.Username = updatedUser.Username;
            newUser.FirstName = updatedUser.FirstName;
            newUser.LastName = updatedUser.LastName;
            //newUser.PasswordSalt = updatedUser.PasswordSalt;
            //newUser.PasswordHash = updatedUser.PasswordHash;
            newUser.Email = updatedUser.Email;
            newUser.PhoneNumber = updatedUser.PhoneNumber;
            newUser.IsAdmin = updatedUser.IsAdmin;

            context.SaveChanges();
            return newUser;
        }

        public User Delete(int id)
        {
            User toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }

        public bool UserExists(string username)
        {
            return context.Users.Any(u => u.Username == username && !u.IsDeleted);
        }

        public bool EmailExists(string email)
        {
            return context.Users.Any(u => u.Email == email && !u.IsDeleted);
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            return context.Users.Any(u => u.PhoneNumber == phoneNumber && !u.IsDeleted);
        }

        public int Count()
        {
            return context.Users.Where(u => !u.IsDeleted).Count();
        }

        public IList<User> FilterBy(UserQueryParameters usersParams)
        {
            IQueryable<User> result = context.Users.Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(usersParams.Username))
            {
                result = result.Where(u => u.Username == usersParams.Username);
            }

            if (!string.IsNullOrEmpty(usersParams.Email))
            {
                result = result.Where(u => u.Email == usersParams.Email);
            }

            if (!string.IsNullOrEmpty(usersParams.PhoneNumber))
            {
                result = result.Where(u => u.PhoneNumber == usersParams.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(usersParams.LicensePlate))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.LicensePlate == usersParams.LicensePlate));
            }

            if (!string.IsNullOrEmpty(usersParams.VIN))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.VIN == usersParams.VIN));
            }

            return result.ToList();
        }
    }
}
