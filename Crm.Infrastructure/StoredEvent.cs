using Crm.Domain;
using System;
using System.Text.Json;

namespace Crm.Infrastructure
{
    public class StoredEvent
    {
        private readonly string eventName;
        private readonly string eventBody;
        private readonly DateTime occuredOn;
        private readonly int id;

        public StoredEvent(DomainEvent domainEvent)
        {
            eventName = domainEvent.GetType().Name;
            eventBody = JsonSerializer.Serialize(domainEvent);
            occuredOn = domainEvent.OccuredOn;
        }

        public int Id => id;
        public string EventName => eventName;
        public string EventBody => eventBody;
        public DateTime OccuredOn => occuredOn;
    }
}
