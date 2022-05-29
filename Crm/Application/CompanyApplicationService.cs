using Crm.Domain.Company;
using Crm.Domain.People;

namespace Crm.Application
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

            return new Company
            {
                Id = company.Id,
                Name = company.Name
            };
        }

        public async Task<List<Company>> GetList()
        {
            var companies = await companyRepository.GetList();

            return companies.Select(company => new Company
            {
                Id = company.Id,
                Name = company.Name
            }).ToList();
        }

        public async Task<List<Owner>> GetOwners(Guid id)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                throw new EntityDoesntExistException();
            }

            return company.Owners.Select(owner => new Owner
            {
                PersonId = owner.PersonId,
                CompanyId = company.Id,
                Name = company.Name,
                IsBeneficial = owner.IsBeneficial,
                Share = owner.Share
            }).ToList();
        }

        public async Task UpdateOwners(Guid id, IEnumerable<Owner> owners)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                throw new EntityDoesntExistException();
            }

            var people = await personRepository.GetList(
                owners.Select(owner => owner.PersonId));

            company.SetOwners(
                owners.Select(
                    owner => new SetOwnersArg(
                        people.Find(person => person.Id == owner.PersonId),
                        owner.Share)));

            await companyRepository.Update(company);

            foreach (var owner in owners)
            {
                var companyOwner = company.GetOwner(owner.PersonId);
                owner.CompanyId = companyOwner.CompanyId;
                owner.IsBeneficial = companyOwner.IsBeneficial;
                owner.Name = companyOwner.Name;
            }
        }
    }
}
