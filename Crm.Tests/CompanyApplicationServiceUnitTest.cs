using Crm.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Crm.Tests
{
    [TestClass]
    public class CompanyApplicationServiceUnitTest
    {
        private CompanyRepositoryFake companyRepository;
        private PersonRepositoryFake personRepository;
        private CompanyApplicationService companyApplication;

        [TestInitialize]
        public void SetUp()
        {
            companyRepository = new CompanyRepositoryFake();
            personRepository = new PersonRepositoryFake();
            companyApplication = new CompanyApplicationService(
                companyRepository,
                personRepository);
        }

        [TestMethod]
        public async Task ReturnsIfExists()
        {
            var company = await companyApplication.Get(Guid.Empty);

            Assert.IsNull(company);
        }

        [TestMethod]
        public async Task ReturnsNullIfMissed()
        {
            var domainCompany = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");
            await companyRepository.Create(domainCompany);

            var company = await companyApplication.Get(domainCompany.Id);

            Assert.IsNotNull(company);
            Assert.AreEqual(company.Id, domainCompany.Id);
            Assert.AreEqual(company.Name, domainCompany.Name);
        }

        [TestMethod]
        public async Task ReturnsList()
        {
            var domainCompany1 = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");
            var domainCompany2 = new Domain.Company.Company(
                Guid.NewGuid(),
                "B");
            await companyRepository.Create(domainCompany1);
            await companyRepository.Create(domainCompany2);

            var companies = await companyApplication.GetList();

            Assert.IsNotNull(companies);
            Assert.AreEqual(companies.Count, 2);

            var company1 = companies.Find(c => c.Id == domainCompany1.Id);
            Assert.IsNotNull(company1);
            Assert.AreEqual(company1.Name, domainCompany1.Name);

            var company2 = companies.Find(c => c.Id == domainCompany2.Id);
            Assert.IsNotNull(company2);
            Assert.AreEqual(company2.Name, domainCompany2.Name);
        }

        [TestMethod]
        public async Task ReturnsOwners()
        {
            var domainPerson1 = new Domain.People.Person(
                Guid.NewGuid(),
                "A1",
                "B1");
            var domainPerson2 = new Domain.People.Person(
                Guid.NewGuid(),
                "A2",
                "B2");
            await personRepository.Create(domainPerson1);
            await personRepository.Create(domainPerson2);

            var domainCompany = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");
            domainCompany.SetOwners(new[]
            {
                new Domain.Company.SetOwnersArg(domainPerson1, 50),
                new Domain.Company.SetOwnersArg(domainPerson2, 50)
            });
            await companyRepository.Create(domainCompany);

            var owners = await companyApplication.GetOwners(domainCompany.Id);

            Assert.IsNotNull(owners);
            Assert.AreEqual(owners.Count, 2);

            var owner1 = owners.Find(c => c.PersonId == domainPerson1.Id);
            Assert.IsNotNull(owner1);
            Assert.AreEqual(owner1.Name, domainPerson1.FullName);

            var owner2 = owners.Find(c => c.PersonId == domainPerson2.Id);
            Assert.IsNotNull(owner2);
            Assert.AreEqual(owner2.Name, domainPerson2.FullName);
        }
    }
}
