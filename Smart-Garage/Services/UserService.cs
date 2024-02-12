using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.Contracts;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Smart_Garage.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository usersRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper autoMapper;
        private readonly IEmailSender emailSender;

        public UserService(IUserRepository usersRepository, IConfiguration configuration, IMapper autoMapper, IEmailSender emailSender)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
            this.autoMapper = autoMapper;
            this.emailSender = emailSender;
        }

        public UserResponseDTO Create(SignUpUserRequestDTO newUser) // Sign Up
        {
            if (usersRepository.UserExists(newUser.Username))
                throw new DuplicationException($"User with name {newUser.Username} already exists.");

            CreatePasswordHash(newUser.Username, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Username = newUser.Username,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return autoMapper.Map<UserResponseDTO>(usersRepository.Create(user));
        }

        public IList<UserResponseDTO> GetAll()
        {
            var users = usersRepository.GetAll();
            return autoMapper.Map<IList<UserResponseDTO>>(users);
        }

        public IList<UserResponseDTO> GetAllNotAdmins() // Get all customers
        {
            var users = usersRepository.GetAllNotAdmins();
            return autoMapper.Map<IList<UserResponseDTO>>(users);
        }

        public IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters, string username)
        {
            IsCurrentUserAdmin(username);

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
            User user = new User();
            user.Username = newData.Username;
            user.FirstName = newData.FirstName;
            user.LastName = newData.LastName;
            user.Email = newData.Email;
            user.PhoneNumber = newData.PhoneNumber;
            user.IsAdmin = newData.IsAdmin;

            User updatedUser = usersRepository.Update(id, user);

            return autoMapper.Map<UserResponseDTO>(updatedUser);
        }

		public void SetPassword(string email, string password)
		{
			CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

			User user = new User();
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			usersRepository.SetPassword(email, user);
		}

		public User Delete(int id, string username)
        {
            IsCurrentUserAdmin(username);

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

        public bool EmailExists(string email)
        {
            return usersRepository.EmailExists(email);
        }

        public void IsCurrentUserAdmin(string currentUser) // "currentUser" is username
        {
            if (!usersRepository.GetByName(currentUser).IsAdmin)
                throw new UnauthorizedOperationException("You are not an admin!");
        }

        public async void SendEmailLogic(SendEmailViewModel userEmail)
        {
            //var reciever = "smart-garage@abv.bg";
            var subject = "Set Password";
            var message = $"<p>Dear User,</p><br><p>It seems that you've forgotten your password for accessing our platform. Not to worry, we've got you covered! To reset your password and regain access to your account, please follow the link below:</p><br><p><a href=\"http://localhost:5068/Admin_Users/SetPassword/{userEmail.Email}\">[Password Reset Link]</a></p><br><p>If you did not initiate this password reset request, please ignore this email or contact our support team immediately.</p><p>Thank you for your attention to this matter.</p><br><p>Best regards,<br>The team of Smart-Garage</p>";

            await emailSender.SendEmailAsync(userEmail.Email, subject, message);
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
