namespace Crm.Domain.People;

public interface IPersonRepository
{
    Task Create(Person person);
    Task<List<Person>> GetList(IEnumerable<Guid> ids);
}