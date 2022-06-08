using Crm.Domain;
using Crm.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crm.Tests;

public class EventStoreFake : IEventStore
{
    private readonly List<StoredEvent> storedEvents = new List<StoredEvent>();

    public Task<StoredEvent> Add(DomainEvent domainEvent)
    {
        var eventName = domainEvent.GetType().Name;
        var eventBody = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
        var storedEvent = new StoredEvent(
            eventName, 
            eventBody, 
            domainEvent.OccuredOn);
        storedEvents.Add(storedEvent);
        return Task.FromResult(storedEvent);
    }

    public Task<List<StoredEvent>> GetList()
    {
        return Task.FromResult(
            storedEvents.OrderBy(e => e.OccuredOn).ToList());
    }

    public Task Remove(StoredEvent storedEvent)
    {
        storedEvents.Remove(storedEvent);
        return Task.CompletedTask;
    }
}