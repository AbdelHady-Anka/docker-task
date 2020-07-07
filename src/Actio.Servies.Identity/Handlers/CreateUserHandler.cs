using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.DomainServices.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Servies.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IBusClient bus,
            IUserService userService,
            ILogger<CreateUserHandler> logger)
        {
            this._logger = logger;
            this._bus = bus;
            this._userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: {command.Email}, {command.Name}");
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _bus.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}