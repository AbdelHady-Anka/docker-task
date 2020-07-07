using System;
using System.Linq;
using System.Threading.Tasks;
using Actio.Api.Repositories;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient _bus;
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IBusClient bus, IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = Guid.Parse(User.Identity.Name);
            await _bus.PublishAsync(command);

            return Accepted($"Activities/{command.Id}");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok((await _activityRepository.GetAllAsync(Guid.Parse(User.Identity.Name)))
                .Select(x => new { x.Id, x.Category, x.CreatedAt, x.Description }));

        [HttpGet("{activityId}")]
        public async Task<IActionResult> Get(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }
            if (activity.UserId != Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }
            return Ok(activity);
        }

    }
}