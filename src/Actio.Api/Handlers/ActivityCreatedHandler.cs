using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            var activity = new Activity
            {
                Id = @event.Id,
                Name = @event.Name,
                Category = @event.Category,
                Description = @event.Description,
                UserId = @event.UserId,
                CreatedAt = @event.CreatedAt
            };

            await _activityRepository.AddAsync(activity);
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}