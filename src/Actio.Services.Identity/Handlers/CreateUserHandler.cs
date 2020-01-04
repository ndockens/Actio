using System;
using System.Threading.Tasks;
using RawRabbit;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService,
            ILogger<CreateUserHandler> logger)
        {
            _logger = logger;
            _busClient = busClient;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                _logger.LogInformation("User created: " + command.Email);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(ex.Message, ex.Code, command.Email));
                _logger.LogError(ex.Message);
            }
        }
    }
}