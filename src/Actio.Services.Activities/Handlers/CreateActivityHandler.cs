using System;
using System.Threading.Tasks;
using RawRabbit;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient,
            IActivityService activityService,
            ILogger<CreateActivityHandler> logger)
        {
            _logger = logger;
            _busClient = busClient;
            _activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            try
            {
                await _activityService.AddAsync(command.Id, command.Name,
                    command.Category, command.Description, command.UserId,
                    command.CreatedAt);

                await _busClient.PublishAsync(new ActivityCreated(command.Id,
                    command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt));
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, command.UserId));
                _logger.LogError(ex.Message);
            }

        }
    }
}