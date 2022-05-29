namespace Crm.Application
{
    public interface ICompanyApplicationService
    {
        Task<Company> Get(Guid id);
        Task<List<Company>> GetList();
        Task<List<Owner>> GetOwners(Guid id);
        Task<List<Owner>> UpdateOwners(Guid id, IEnumerable<Owner> owners);
    }
}