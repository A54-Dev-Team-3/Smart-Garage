﻿using Microsoft.Extensions.Configuration;
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

        public UsersService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
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

        public string Login(UserRequestDTO user)
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
            return usersRepository.UserExists(username);
        }

        public bool IsCurrentUserAdmin(string currentUser) // "currentUser" is username
        {
            return usersRepository.GetByName(currentUser).IsAdmin;
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
    }
}
