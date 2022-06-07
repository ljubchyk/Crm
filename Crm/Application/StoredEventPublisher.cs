using Crm.Infrastructure;

namespace Crm.Application
{
    public class StoredEventPublisher : BackgroundService
    {
        private readonly IEventStore eventStore;

        public StoredEventPublisher(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var storedEvents = await eventStore.GetList();

                throw new NotImplementedException();
                await Task.Delay(500);
            }
        }
    }
}
