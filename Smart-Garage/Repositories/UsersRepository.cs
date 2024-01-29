using Microsoft.EntityFrameworkCore;

using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;

namespace Smart_Garage.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SGContext context;

        public UsersRepository(SGContext context)
        {
            this.context = context;
        }

        public IList<User> GetAll()
        {
            return context.Users.Where(u=> !u.IsDeleted).ToList();
        }

        public User GetById(int id)
        {
            return context.Users
            .FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
            throw new EntityNotFoundException($"User with id:{id} not found.");
        }

        public User GetByName(string name)
        {
            return context.Users.FirstOrDefault(u => u.Username == name && !u.IsDeleted) ??
               throw new EntityNotFoundException($"User with username:{name} is not found.");
        }

        public User Create(User newUser)
        {

            var username = newUser.Username;
            var email = newUser.Email;

            if (!UserExists(username))
            {
                if (!EmailExists(email))
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    return newUser;
                }
                else
                    throw new DuplicationException($"Username with the name: {username} already exists !");
            }
            else
                throw new DuplicationException($"Email with the name: {email} already exists !");
            
        }

        public User Update(int id, User updatedUser)
        {
            User newUser = context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User to update with id:{id} not found !");

            // Username should not be able to be updated
            newUser.Email = updatedUser.Email;
            newUser.PhoneNumber = updatedUser.PhoneNumber;

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
            // TODO
            throw new NotImplementedException();
        }
    }
}
