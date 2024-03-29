﻿using Crm.Domain.Companies;
using Crm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Tests;

public class CompanyRepositoryFake : ICompanyRepository
{
    private readonly StorageFake<Company> storage;

    public CompanyRepositoryFake(IEventStore eventStore)
    {
        storage = new StorageFake<Company>(eventStore);
    }

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