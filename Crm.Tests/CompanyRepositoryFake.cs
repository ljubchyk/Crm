using Crm.Domain.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CompanyRepositoryFake : ICompanyRepository
{
    private readonly StorageFake<Company> storage = new StorageFake<Company>();
    private readonly Dictionary<Guid, Company> companies = new Dictionary<Guid, Company>();

    public Task Create(Company company)
    {
        return storage.Create(company.Id, company);
    }

    public Task<Company> Get(Guid id)
    {
        return storage.Get(id);
    }

    public Task<List<Company>> GetList()
    {
        return Task.FromResult(
            storage.Query().ToList());
    }

    public Task Update(Company company)
    {
        return storage.Update(company.Id, company);
    }
}
