namespace Crm.Domain.People;

public interface IPersonRepository
{
    Task Create(Person person);
    Task Update(Person person);
    Task<Person> Get(Guid id);
    Task<List<Person>> GetList();
    Task<List<Person>> GetList(IEnumerable<Guid> ids);
}