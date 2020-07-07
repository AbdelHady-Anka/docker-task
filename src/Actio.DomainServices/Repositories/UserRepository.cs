using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Domain.Entities;
using Actio.DomainServices.Services;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.DomainServices.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEncrypter _encrypter;
        private readonly IMongoDatabase _mongoDb;

        public UserRepository(IMongoDatabase mongoDb, IEncrypter encrypter)
        {
            this._mongoDb = mongoDb;
            this._encrypter = encrypter;
        }

        public async Task AddAsync(User user)
        {
            await Collection.InsertOneAsync(user);
        }

        private string InitiatePassword(string value)
        {
            var salt = _encrypter.GetSalt(value);
            var password = _encrypter.GetHash(value, salt);
            return password;
        }

        public async Task<User> GetByEmailAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());

        public async Task<User> GetByIdAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                // throw new ActioException("user_not_found",
                // $"There is no user with {email} email");
                return false;
            }
            if (ValidatePassowrd(password, user.Password, user.Salt))
            {
                // throw new ActioException("invalid_password",
                // $"password is not valid");
                return false;
            }
            return true;
        }
        private bool ValidatePassowrd(string password, string passwordHash, string passwordSalt)
            => passwordHash.Equals(_encrypter.GetHash(password, passwordSalt));

        private IMongoCollection<User> Collection
            => _mongoDb.GetCollection<User>("Users");
    }
}