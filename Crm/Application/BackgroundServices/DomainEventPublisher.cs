using Crm.Infrastructure;
using MassTransit;

namespace Crm.Application.BackgroundServices
{
    public class DomainEventPublisher : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IBus bus;

        public DomainEventPublisher(IServiceProvider serviceProvider, IBus bus)
        {
            this.serviceProvider = serviceProvider;
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var eventStore = scope.ServiceProvider.GetRequiredService<IEventStore>();
                await PublisEvents(eventStore, stoppingToken);
                await Task.Delay(500, stoppingToken);
            }
        }

        private async Task PublisEvents(IEventStore eventStore, CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }

            var storedEvents = await eventStore.GetList();
            foreach (var storedEvent in storedEvents)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return;
                }

                await bus.Publish(storedEvent.EventBody, stoppingToken);
                await eventStore.Remove(storedEvent);
            }
        }
    }
}
