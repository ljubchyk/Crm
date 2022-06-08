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

        public StoredEvent(string eventName, string eventBody, DateTime occuredOn)
        {
            this.eventName = eventName;
            this.eventBody = eventBody;
            this.occuredOn = occuredOn;
        }

        public int Id => id;
        public string EventName => eventName;
        public string EventBody => eventBody;
        public DateTime OccuredOn => occuredOn;
    }
}
