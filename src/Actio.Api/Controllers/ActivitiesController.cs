using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Actio.Api.Repositories;
using Actio.Common.Auth;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IBusClient busClient,
            IActivityRepository activityRepository)
        {
            _busClient = busClient;
            _activityRepository = activityRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var activities = await _activityRepository.BrowseAsync(userId);

            return Json(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = Guid.Parse(User.Identity.Name);
            var activity = await _activityRepository.GetAsync(id);

            if (userId != activity.UserId) return Unauthorized();

            return Json(activity);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}