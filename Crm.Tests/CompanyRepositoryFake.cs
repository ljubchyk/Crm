using Crm.Domain.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CompanyRepositoryFake : ICompanyRepository
{
    private readonly Dictionary<Guid, Company> companies = new Dictionary<Guid, Company>();
    
    public Task Create(Company company)
    {
        companies.Add(company.Id, company);
        return Task.CompletedTask;
    }

    public Task<Company> Get(Guid id)
    {
        companies.TryGetValue(id, out Company company);
        return Task.FromResult(company);
    }

    public Task<List<Company>> GetList()
    {
        return Task.FromResult(companies.Values.ToList());
    }

    public Task Update(Company company)
    {
        return Task.CompletedTask;
    }
}
