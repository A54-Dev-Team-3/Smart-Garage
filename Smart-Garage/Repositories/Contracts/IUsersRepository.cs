using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IUsersRepository
    {
        IList<User> GetAll();
        User GetById(int id);
        User GetByName(string name);
        User Create(User newUser); // Register
        User Update(int id, User updatedUser);
        User Delete(int id);
        bool UserExists(string username);
        bool EmailExists(string email);
        bool PhoneNumberExists(string phoneNumber);
        int Count();
    }
}
