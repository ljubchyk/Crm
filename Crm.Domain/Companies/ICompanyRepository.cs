namespace Crm.Domain.Companies;

public interface ICompanyRepository
{
    Task Create(Company company);
    Task Update(Company company);
    Task<Company> Get(Guid id);
    Task<List<Company>> GetList();
}
