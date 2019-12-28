using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers
{
    public class AuthenticateUserHandler : ICommandHandler<AuthenticateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticateUserHandler> _logger;

        public AuthenticateUserHandler(IBusClient busClient,
            IUserService userService,
            ILogger<AuthenticateUserHandler> logger)
        {
            _logger = logger;
            _busClient = busClient;
            _userService = userService;
        }

        public async Task HandleAsync(AuthenticateUser command)
        {
            try
            {
                var jwt = await _userService.LoginAsync(command.Email, command.Password);
                _logger.LogInformation("User authenticated: " + command.Email);
                await _busClient.PublishAsync(new UserAuthenticated(command.Email, jwt.Token));
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new AuthenticateUserRejected(ex.Message, ex.Code, command.Email));
                _logger.LogError(ex.Message);
            }
        }
    }
}