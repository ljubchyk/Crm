using Crm.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class GetOwnersUnitTest
    {
        private CompanyRepositoryFake companyRepository;
        private CompanyApplicationService companyApplication;

        [TestInitialize]
        public void SetUp()
        {
            companyRepository = new CompanyRepositoryFake();
            companyApplication = new CompanyApplicationService(
                companyRepository,
                new PersonRepositoryFake());
        }

        [TestMethod]
        public async Task Returns()
        {
            var domainPerson1 = new Domain.People.Person(
                Guid.NewGuid(),
                "A1",
                "B1");
            var domainPerson2 = new Domain.People.Person(
                Guid.NewGuid(),
                "A2",
                "B2");

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
            Assert.AreEqual(owner1.Share, domainCompany.GetOwner(domainPerson1.Id).Share);

            var owner2 = owners.Find(c => c.PersonId == domainPerson2.Id);
            Assert.IsNotNull(owner2);
            Assert.AreEqual(owner2.Name, domainPerson2.FullName);
            Assert.AreEqual(owner2.Share, domainCompany.GetOwner(domainPerson2.Id).Share);
        }

        [TestMethod]
        public async Task ReturnsEmptyIfOwnersMissed()
        {
            var domainCompany = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");
            await companyRepository.Create(domainCompany);

            var owners = await companyApplication.GetOwners(domainCompany.Id);
            Assert.IsNotNull(owners);
            Assert.AreEqual(owners.Count, 0);
        }

        [TestMethod]
        public async Task ReturnsNullIfCompanyMissed()
        {
            var domainCompany = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");
            await companyRepository.Create(domainCompany);

            var owners = await companyApplication.GetOwners(Guid.Empty);
            Assert.IsNull(owners);
        }
    }
}
