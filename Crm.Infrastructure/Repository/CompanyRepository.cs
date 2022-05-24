using Crm.Domain.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{
    public Task Create(Company company)
    {
        throw new NotImplementedException();
    }

    public Task<Company> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Company>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task Update(Company company)
    {
        throw new NotImplementedException();
    }
}
