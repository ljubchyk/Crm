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
        companies.Add(company.Id, new Company(company));
        return Task.CompletedTask;
    }

    public Task<Company> Get(Guid id)
    {
        companies.TryGetValue(id, out Company company);
        if (company is null)
        {
            return Task.FromResult(company);
        }

        return Task.FromResult(new Company(company));
    }

    public Task<List<Company>> GetList()
    {
        return Task.FromResult(
            companies.Values.Select(
                company => new Company(company)).ToList());
    }

    public Task Update(Company company)
    {
        var isRemoved = companies.Remove(company.Id);
        if (!isRemoved)
        {
            throw new Exception($"Unknown company with id: {company.Id}");
        }

        companies.Add(company.Id, new Company(company));
        return Task.CompletedTask;
    }
}
