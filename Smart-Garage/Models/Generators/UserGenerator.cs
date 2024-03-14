using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics.Metrics;
using System;
using System.Security.Cryptography;

namespace Smart_Garage.Models.Generators
{
    public static class UserGenerator
    {
        internal static IList<User> CreateUsers()
        {
            var ids = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
            };

            var usernames = new string[]
            {
                "admin",
                "george123",
                "alexander99",
                "ben",
                "ethan Mit",
                "The Oliver_6"
            };

            var firstnames = new string[]
            {
                "admin",
                "George",
                "Alexander",
                "Benjamin",
                "Ethan",
                "Oliver"
            };

            var lastnames = new string[]
            {
                "admin",
                "Smith",
                "Parker",
                "Hayes",
                "Mitchell",
                "Reynolds"
            };

            var emails = new string[]
            {
                "admin@gmail.com",
                "george@gmail.com",
                "alexander@gmail.com",
                "hayes@gmail.com",
                "mitchell@gmail.com",
                "reynolds@gmail.com"
            };

            var phoneNumbers = new string[]
            {
                "1234567890",
                "1234567891",
                "1234567892",
                "1234567893",
                "1234567894",
                "1234567895",
            };

            var areDeleted = new bool[]
            {
                false,
                false,
                false,
                false,
                false,
                false
            };

            var areAdmin = new bool[]
            {
                true,
                false,
                false,
                false,
                false,
                false
            };

            var users = new List<User>();

            for (int i = 0; i < 6; i++)
            {
                var newUser = new User();
                newUser.Id = ids[i];
                newUser.Username = usernames[i];
                newUser.FirstName = firstnames[i];
                newUser.LastName = lastnames[i];
                CreatePasswordHash(newUser.Username, out byte[] passwordHash, out byte[] passwordSalt);
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                newUser.Email = emails[i];
                newUser.PhoneNumber = phoneNumbers[i];
                newUser.IsDeleted = areDeleted[i];
                newUser.IsAdmin = areAdmin[i];

                users.Add(newUser);
            }
            return users;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}