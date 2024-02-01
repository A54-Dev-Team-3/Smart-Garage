using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Smart_Garage.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper autoMapper;

        public UsersService(IUsersRepository usersRepository, IConfiguration configuration, IMapper autoMapper)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
            this.autoMapper = autoMapper;
        }

        public UserResponseDTO Create(SignUpUserRequestDTO newUser) // Sign Up
        {
            if (usersRepository.UserExists(newUser.Username))
                throw new DuplicationException($"User with name {newUser.Username} already exists.");

            CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Username = newUser.Username,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber
            };
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            return autoMapper.Map<UserResponseDTO>(usersRepository.Create(user));
        }

        public IList<UserResponseDTO> GetAll()
        {
            var users = usersRepository.GetAll();
            return autoMapper.Map<IList<UserResponseDTO>>(users);
        }

        public IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters, string username)
        {
            if (!IsCurrentUserAdmin(username))
                throw new UnauthorizedAccessException("You are not authorized !");

            return usersRepository.FilterBy(filterParameters)
                            .Select(u => autoMapper.Map<UserResponseDTO>(u))
                            .ToList();
        }

        public UserResponseDTO GetById(int id)
        {
            User user = usersRepository.GetById(id);
            return autoMapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO GetByName(string username)
        {
            User user = usersRepository.GetByName(username);
            return autoMapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO Update(int id, UpdateUserRequestDTO newData, string username)
        {
            IsCurrentUserOwner(id, username);

            CreatePasswordHash(newData.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var tmpUser = GetById(id);

            User user = new User();
            user.Username = newData.Username;
            user.FirstName = tmpUser.FirstName;
            user.LastName = tmpUser.LastName;
            user.Email = newData.Email;
            user.PhoneNumber = newData.PhoneNumber;
            user.IsAdmin = newData.IsAdmin;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            User updatedUser = usersRepository.Update(id, user);

            return autoMapper.Map<UserResponseDTO>(updatedUser);
        }

        public User Delete(int id, string username)
        {
            IsCurrentUserOwner(id, username);

            return usersRepository.Delete(id);
        }

        public string Login(LoginUserRequestDTO user)
        {
            if (!usersRepository.UserExists(user.Username))
                throw new EntityNotFoundException("User not found.");

            User registeredUser = usersRepository.GetByName(user.Username);
            if (!VerifyPasswordHash(
                user.Password,
                registeredUser.PasswordHash,
                registeredUser.PasswordSalt))
            {
                throw new WrongPasswordException("Wrong password");
            }

            var token = CreateToken(registeredUser);
            return token;
        }

        public int GetCount()
        {
            return usersRepository.Count();
        }
        public bool UserExists(string username)
        {
            return usersRepository.UserExists(username);
        }

        public bool IsCurrentUserAdmin(string currentUser) // "currentUser" is username
        {
            return usersRepository.GetByName(currentUser).IsAdmin;
        }

        private void IsCurrentUserOwner(int id, string currentUser) // "currentUser" is username
        {
            if (GetById(id).Username != currentUser)
                throw new UnauthorizedOperationException("You are not the owner of the account!");
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public User Authenticate(string username)
        {
            return usersRepository.GetByName(username);
        }
    }
}
