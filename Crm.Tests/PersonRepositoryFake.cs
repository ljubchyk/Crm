using Crm.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//public class PersonRepositoryFake : IPersonRepository
//{
//    private readonly Dictionary<Guid, Person> people = new Dictionary<Guid, Person>();

//    public Task Create(Person person)
//    {
//        people.Add(person.Id, person);
//        return Task.CompletedTask;
//    }

//    public Task<List<Person>> GetList(IEnumerable<Guid> ids)
//    {
//        return Task.FromResult(
//            people.Values.Where(
//                v => ids.Contains(v.Id)).ToList());
//    }
//}

public class PersonRepositoryFake : IPersonRepository
{
    private readonly StorageFake<Person> storage = new StorageFake<Person>();
    private readonly Dictionary<Guid, Person> people = new Dictionary<Guid, Person>();

    public Task Create(Person person)
    {
        return storage.Create(person.Id, person);
    }

    public Task<List<Person>> GetList(IEnumerable<Guid> ids)
    {
        return Task.FromResult(
            storage.Query().Where(
                v => ids.Contains(v.Id)).ToList());
    }
}