namespace Crm.Domain.Companies;

public interface ICompanyRepository
{
    Task Create(Company company);
    Task Update(Company company);
    Task Update(ICollection<Company> companies);
    Task<Company> Get(Guid id);
    Task<List<Company>> GetList();
    Task<List<Company>> GetListWithOwner(Guid personId);
}
