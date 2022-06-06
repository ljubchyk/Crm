using Crm.Domain.Companies;
using Crm.Domain.People;

namespace Crm.Application.Companies
{
    public class CompanyApplicationService : ICompanyApplicationService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IPersonRepository personRepository;

        public CompanyApplicationService(ICompanyRepository companyRepository, IPersonRepository personRepository)
        {
            this.companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<Company> Get(Guid id)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                return null;
            }

            return Company.Create(company);
        }

        public async Task<List<Company>> GetList()
        {
            var companies = await companyRepository.GetList();
            return companies.Select(company => Company.Create(company)).ToList();
        }

        public async Task Create(Company model)
        {
            var company = new Domain.Companies.Company(
                Guid.NewGuid(),
                model.Name);

            await companyRepository.Create(company);
            Company.Fill(model, company);
        }

        public async Task Update(Company model)
        {
            var company = await companyRepository.Get(model.Id);
            if (company is null)
            {
                throw new InvalidOperationException($"Unknown company with id: {model.Id}");
            }

            company.Rename(model.Name);
            await companyRepository.Update(company);
            Company.Fill(model, company);
        }

        public async Task<List<Owner>> GetOwners(Guid id)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                return null;
            }

            return company.Owners.Select(owner => Owner.Create(owner)).ToList();
        }

        public Task UpdateOwners(Guid id, params Owner[] owners)
        {
            return UpdateOwners(id, (IEnumerable<Owner>)owners);
        }

        public async Task UpdateOwners(Guid id, IEnumerable<Owner> owners)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                throw new InvalidOperationException($"Unknown company with id: {id}");
            }

            var people = await personRepository.GetList(
                owners.Select(owner => owner.PersonId));

            company.SetOwners(
                owners.Select(
                    owner =>
                    {
                        var person = people.Find(person => person.Id == owner.PersonId);
                        if (person is null)
                        {
                            throw new InvalidOperationException($"Unknown person with id: {owner.PersonId}");
                        }
                        return new OwnerArg(
                            person,
                            owner.Share);
                    }).ToArray());

            await companyRepository.Update(company);

            foreach (var owner in owners)
            {
                Owner.Fill(owner, company.GetOwner(owner.PersonId));
            }
        }
    }
}
