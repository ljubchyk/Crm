namespace Crm.Domain.Company;

public interface ICompanyRepository
{
    Task Create(Company company);
    Task Update(Company company);
    Task<Company> Get(Guid id);
    Task<List<Company>> GetList();
}
