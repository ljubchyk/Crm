using Crm.Domain.People;

namespace Crm.Application
{
    public interface IPersonApplicationService
    {
        Task<Person> Get(Guid id);
        Task<List<Person>> GetList();
        Task Create(Person person);
        Task Update(Person person);
    }
}