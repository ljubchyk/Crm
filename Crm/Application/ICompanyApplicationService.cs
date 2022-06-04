namespace Crm.Application
{
    public interface ICompanyApplicationService
    {
        Task<Company> Get(Guid id);
        Task<List<Company>> GetList();
        Task Create(Company company);
        Task Update(Company company);
        Task<List<Owner>> GetOwners(Guid id);
        Task UpdateOwners(Guid id, IEnumerable<Owner> owners);
    }
}