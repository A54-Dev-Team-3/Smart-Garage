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

        public IList<User> FilterBy(UserQueryParameters usersParams)
        {

            IQueryable<User> result = context.Users
                .Where(u => !u.IsAdmin && !u.IsDeleted)
                .Include(u => u.Vehicles)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Brand);

            if (!string.IsNullOrEmpty(usersParams.Username))
            {
                result = result.Where(u => u.Username.Contains(usersParams.Username));
            }

            if (!string.IsNullOrEmpty(usersParams.FirstName))
            {
                result = result.Where(u => u.FirstName.Contains(usersParams.FirstName));
            }

            if (!string.IsNullOrEmpty(usersParams.LastName))
            {
                result = result.Where(u => u.LastName.Contains(usersParams.LastName));
            }

            if (!string.IsNullOrEmpty(usersParams.Email))
            {
                result = result.Where(u => u.Email.Contains(usersParams.Email));
            }

            if (!string.IsNullOrEmpty(usersParams.PhoneNumber))
            {
                result = result.Where(u => u.PhoneNumber.Contains(usersParams.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(usersParams.LicensePlate))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.LicensePlate.Contains(usersParams.LicensePlate)));
            }

            if (!string.IsNullOrEmpty(usersParams.VIN))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.VIN.Contains(usersParams.VIN)));
            }

            if (!string.IsNullOrEmpty(usersParams.Model))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.Model.Name.Contains(usersParams.Model)));
            }

            if (!string.IsNullOrEmpty(usersParams.Brand))
            {
                result = result.Where(u => u.Vehicles.Any(v => v.Model.Brand.Name.Contains(usersParams.Brand)));
            }

            return result.ToList();
        }

        public IList<User> GetAllNotAdmins()
        {
            return context.Users.Where(u => !u.IsAdmin && !u.IsDeleted).ToList();
        }

        public User GetById(int id)
        {
            return context.Users
                .Include(u => u.Vehicles)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Brand)
                .FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");
        }

        public User GetByName(string name)
        {
            return context.Users
                .Include(u => u.Vehicles)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Brand)
                .FirstOrDefault(u => u.Username == name && !u.IsDeleted) ??
               throw new EntityNotFoundException($"User with username:\"{name}\" is not found.");
        }

		public User GetByEmail(string email)
		{
			return context.Users.FirstOrDefault(u => u.Email == email && !u.IsDeleted) ??
			   throw new EntityNotFoundException($"User with email:\"{email}\" is not found.");
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
            var newUser = context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");

            newUser.Username = updatedUser.Username;
            newUser.FirstName = updatedUser.FirstName;
            newUser.LastName = updatedUser.LastName;
            newUser.Email = updatedUser.Email;
            newUser.PhoneNumber = updatedUser.PhoneNumber;
            newUser.IsAdmin = updatedUser.IsAdmin;

            context.SaveChanges();
            return newUser;
        }

		public User SetPassword(string email, User updatedUser)
        {
            var userToUpdate = GetByEmail(email);

			userToUpdate.PasswordSalt = updatedUser.PasswordSalt;
			userToUpdate.PasswordHash = updatedUser.PasswordHash;
			context.SaveChanges();
			return userToUpdate;
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

        
    }
}
