using System;
using Actio.Common.Exceptions;

namespace Actio.Domain.Entities
{
    public class User
    {
        protected User()
        {
        }

        public User(string name, string email, string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("empty_user_email",
                    $"User email can not be empty");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_user_name",
                    $"User name can not be empty");
            }
            this.Name = name;
            this.Email = email.ToLowerInvariant();
            this.Password = password;
            this.Salt = salt;
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

    }

}