using Crm.Domain.People;
using Crm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Tests;

public class PersonRepositoryFake : IPersonRepository
{
    private readonly StorageFake<Person> storage;

    public PersonRepositoryFake(IEventStore eventStore)
    {
        storage = new StorageFake<Person>(eventStore);
    }

    public Task Create(Person person)
    {
        return storage.Create(person.Id, person);
    }

    public Task<Person> Get(Guid id)
    {
        return storage.Get(id);
    }

    public Task<List<Person>> GetList(IEnumerable<Guid> ids)
    {
        return Task.FromResult(
            storage.Query().Where(
                v => ids.Contains(v.Id)).ToList());
    }

    public Task<List<Person>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task Update(Person person)
    {
        return storage.Update(person.Id, person);
    }
}