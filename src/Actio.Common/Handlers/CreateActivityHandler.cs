using System;
using System.Threading.Tasks;
using RawRabbit;
using Actio.Common.Commands;
using Actio.Common.Events;

namespace Actio.Common.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine("Creating activity");
            await _busClient.PublishAsync(new ActivityCreated(command.Id,
                command.UserId, command.Category, command.Name,
                command.Description, command.CreatedAt));
        }
    }
}