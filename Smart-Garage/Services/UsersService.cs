using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IList<User> GetAll()
        {
            return usersRepository
                .GetAll()
               .ToList();
        }

        public User GetUser(int id)
        {
            return usersRepository.GetById(id);
        }

        public User GetUser(string username)
        {
            return usersRepository.GetByName(username);
        }

        public User Create(UserRequestDTO newUser) // Sign Up
        {
            if (usersRepository.UserExists(newUser.Username))
                throw new NameDuplicationException($"User with name {newUser.Username} already exists.");


            CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Username = newUser.Username,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                IsAdmin = false

                // TODO
                //FirstName = newUser.FirstName,
                //LastName = newUser.LastName
            };
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            return usersRepository.Create(user);
        }

        public User Update(int id, User user)
        {
            return usersRepository.Update(id, user);
        }

        public User Delete(int id, string username)
        {
            User user = usersRepository.GetByName(username);

            if (!user.IsAdmin)
                throw new UnauthorizedOperationException($"User is not authorized.");

            return usersRepository.Delete(id);
        }

        public string Login(User user)
        {
            if (!usersRepository.UserExists(user.Username))
                throw new EntityNotFoundException("User not found.");

            // TODO: implement PasswordHas and PasswordSalt and uncomment these lines of code

            //User registeredUser = usersRepository.GetByName(user.Username);
            //if (!VerifyPasswordHash(
            //    user.Password,
            //    registeredUser.PasswordHash,
            //    registeredUser.PasswordSalt))
            //{
            //    throw new WrongPasswordException("Wrong password");
            //}


            //var token = CreateToken(registeredUser);
            //return token;
            throw new NotImplementedException();
        }

        public IList<User> FilterBy(UserQueryParameters usersParams)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return usersRepository.Count();
        }
        public bool UserExists(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsCurrentUserAdmin(string currentUser) // "currentUser" is username
        {
            return usersRepository.GetByName(currentUser).IsAdmin;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            throw new NotImplementedException();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            throw new NotImplementedException();
        }
    }
}
