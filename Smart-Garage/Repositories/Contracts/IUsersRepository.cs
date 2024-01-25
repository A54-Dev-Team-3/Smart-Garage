using Smart_Garage.Models;

namespace Smart_Garage.Repositories.Contracts
{
    public interface IUsersRepository
    {
        IList<User> GetAll();
        //IList<User> FilterBy(UserQueryParameters usersParams);
        User GetById(int id);
        User GetByName(string name);
        User Create(User newUser); // Register
        User Update(int id, User user);
        User Delete(int id);
        User Block(int id);
        bool UserExists(string name);
        int Count();
    }
}
