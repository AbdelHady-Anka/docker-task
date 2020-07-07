using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using Microsoft.Extensions.Logging;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivityCreatedHandler> _logger;

        public ActivityCreatedHandler(IActivityRepository activityRepository,
            ILogger<ActivityCreatedHandler> logger)
        {
            this._logger = logger;
            this._activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity
            {
                Id = @event.Id,
                Category = @event.Category,
                CreatedAt = @event.CreatedAt,
                Description = @event.Description,
                Name = @event.Name,
                UserId = @event.UserId
            });
            _logger.LogInformation($"Activity Created: {@event.Name}");
        }
    }
}