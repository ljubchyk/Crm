using Crm.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PersonRepositoryFake : IPersonRepository
{
    private readonly Dictionary<Guid, Person> people = new Dictionary<Guid, Person>();

    public Task Create(Person person)
    {
        people.Add(person.Id, person);
        return Task.CompletedTask;
    }

    public Task<List<Person>> GetList(IEnumerable<Guid> ids)
    {
        return Task.FromResult(
            people.Values.Where(
                v => ids.Contains(v.Id)).ToList());
    }
}