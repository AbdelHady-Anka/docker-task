using System.Threading.Tasks;
using Actio.Common.Events;
using Microsoft.Extensions.Logging;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            this._logger = logger;
        }

        public Task HandleAsync(UserCreated @event)
        {
            _logger.LogInformation($"user with email: {@event.Email} has been created");
            return Task.CompletedTask;
        }
    }
}