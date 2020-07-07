using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace Actio.Common.Services
{
    public class BusBuilder : BuilderBase
    {
        private IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            _webHost = webHost;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost.Services.CreateScope()
            .ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            _bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost.Services.CreateScope()
            .ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();

            _bus.WithEventHandlerAsync(handler);
            return this;
        }

        public override IServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}