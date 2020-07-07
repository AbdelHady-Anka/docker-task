using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Domain.Entities;
using Actio.DomainServices.Repositories;

namespace Actio.DomainServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            this._userRepository = userRepository;
            this._encrypter = encrypter;
            this._jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new ActioException("invalid_credentials",
                    $"Invalid credentials.");
            }
            if (await _userRepository.ValidateUser(email, password))
            {
                throw new ActioException("invalid_credentials",
                    $"Invalid credentials.");
            }
            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("empty_password",
                    $"password can not be empty.");
            }
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new ActioException("email_in_use",
                    $"Email: {email} is already in use.");
            }
            var salt = _encrypter.GetSalt(password);
            password = _encrypter.GetHash(password, salt);

            user = new User(name, email, password, salt);
            await _userRepository.AddAsync(user);
        }
    }
}