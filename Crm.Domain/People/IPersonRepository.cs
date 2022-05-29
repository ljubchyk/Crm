namespace Crm.Domain.People;

public interface IPersonRepository
{
    Task<List<Person>> GetList(IEnumerable<Guid> ids);
}