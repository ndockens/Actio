using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Services
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
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
                throw new ActioException("user_already_exists",
                    "User account already exists.");

            user = new User(email, name);
            user.SetPassword(password, _encrypter);

            await _userRepository.AddAsync(user);
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
                throw new ActioException("user_does_not_exist",
                    "User account does not exist.");

            var passwordIsCorrect = user.ValidatePassword(password, _encrypter);

            if (!passwordIsCorrect)
                throw new ActioException("incorrect_password", "Password is not correct.");

            return _jwtHandler.Create(user.Id);
        }
    }
}