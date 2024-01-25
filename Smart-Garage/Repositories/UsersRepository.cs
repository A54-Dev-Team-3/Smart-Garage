using Microsoft.EntityFrameworkCore;

using Smart_Garage.Exceptions;
using Smart_Garage.Models;
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
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users
                .FirstOrDefault(u => u.Id == id) ??
                throw new EntityNotFoundException($"User with id:{id} not found.");
        }

        public User GetByName(string name)
        {
            return context.Users.FirstOrDefault(u => u.Username == name) ??
               throw new EntityNotFoundException($"User with username:{name} is not found.");
        }

        public User Create(User newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser;
        }

        public User Update(int id, User user)
        {
            User newUser = context.Users.FirstOrDefault(u => u.Id == id) ??
                throw new EntityNotFoundException($"User to update with id:{id} not found.");

            // Username should not be able to be updated
            newUser.Email = user.Email;
            newUser.PhoneNumber = user.PhoneNumber;

            context.SaveChanges();
            return newUser;
        }

        public User Delete(int id)
        {
            User toDelete = GetById(id);
            context.Remove(toDelete);
            context.SaveChanges();
            return toDelete;
        }

        public bool UserExists(string name)
        {
            return context.Users.Any(u => u.Username == name);
        }

        public int Count()
        {
            return context.Users.Count();
        }    
    }
}
