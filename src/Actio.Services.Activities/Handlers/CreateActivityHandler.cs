using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.DomainServices.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly IActivityService _activityService;
        private readonly ILogger _logger;

        public CreateActivityHandler(IBusClient bus,
            IActivityService activityService,
            ILogger<CreateActivityHandler> logger)
        {
            _logger = logger;
            _bus = bus;
            _activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating Activiy: {command.Name}");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category
                , command.Name, command.Description, command.CreatedAt);
                await _bus.PublishAsync(new ActivityCreated(command.Id, command.UserId,
                            command.Category, command.Name, command.Description, command.CreatedAt)
                            );
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(ex.Message, "error"));
                _logger.LogError(ex.Message);
            }
        }
    }
}