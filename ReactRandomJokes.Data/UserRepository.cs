using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactRandomJokes.Data
{
   public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            using var ctx = new JokesDbContext(_connectionString);
            user.PasswordHash = hash;
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (isValidPassword)
            {
                return user; 
            }

            return null;
        }

        public User GetByEmail(string email)
        {
            using var ctx = new JokesDbContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }
    }

}
