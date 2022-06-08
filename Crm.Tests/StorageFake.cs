using Crm.Domain;
using Crm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Tests;

public class StorageFake<T> 
    where T : Entity
{
    private readonly Dictionary<Guid, T> data = new();
    private readonly IEventStore eventStore;

    public StorageFake(IEventStore eventStore)
    {
        this.eventStore = eventStore;
    }

    public Task Create(Guid id, T item)
    {
        var copy = (T)Activator.CreateInstance(typeof(T), item);
        data.Add(id, copy);

        StoreEvents(item);
        return Task.CompletedTask;
    }

    public Task<T> Get(Guid id)
    {
        data.TryGetValue(id, out T item);
        if (item is null)
        {
            return Task.FromResult(item);
        }

        var copy = (T)Activator.CreateInstance(typeof(T), item);
        return Task.FromResult(copy);
    }

    public IEnumerable<T> Query()
    {
        return data.Values.Select(
            item => (T)Activator.CreateInstance(typeof(T), item));
    }

    public Task Update(Guid id, T item)
    {
        var isRemoved = data.Remove(id);
        if (!isRemoved)
        {
            throw new Exception($"Unknown data with id: {id}");
        }

        var copy = (T)Activator.CreateInstance(typeof(T), item);
        data.Add(id, copy);

        StoreEvents(item);
        return Task.CompletedTask;
    }

    private void StoreEvents(T item)
    {
        foreach (var domainEvent in item.DomainEvents)
        {
            eventStore.Add(domainEvent);
        }
    }
}
