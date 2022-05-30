using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StorageFake<T>
{
    private readonly Dictionary<Guid, T> data = new Dictionary<Guid, T>();

    public Task Create(Guid id, T item)
    {
        var copy = (T)Activator.CreateInstance(typeof(T), item);
        data.Add(id, copy);
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
        return Task.CompletedTask;
    }
}
