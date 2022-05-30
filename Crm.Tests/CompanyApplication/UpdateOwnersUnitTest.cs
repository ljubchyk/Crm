using Crm.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class UpdateOwnersUnitTest
    {
        private PersonRepositoryFake personRepository;
        private CompanyRepositoryFake companyRepository;
        private CompanyApplicationService companyApplication;

        [TestInitialize]
        public void SetUp()
        {
            personRepository = new PersonRepositoryFake();
            companyRepository = new CompanyRepositoryFake();
            companyApplication = new CompanyApplicationService(
                companyRepository,
                personRepository);
        }

        [TestMethod]
        public async Task Updates()
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

            var domainPerson3 = new Domain.People.Person(
                Guid.NewGuid(),
                "A3",
                "B3");
            var domainPerson4 = new Domain.People.Person(
                Guid.NewGuid(),
                "A4",
                "B4");

            await personRepository.Create(domainPerson3);
            await personRepository.Create(domainPerson4);

            var owner1 = new Owner
            {
                PersonId = domainPerson3.Id,
                Share = 25
            };
            var owner2 = new Owner
            {
                PersonId = domainPerson4.Id,
                Share = 75
            };
            await companyApplication.UpdateOwners(
                domainCompany.Id,
                new[]
                {
                    owner1,
                    owner2
                });

            Assert.AreEqual(owner1.CompanyId, domainCompany.Id);
            Assert.AreEqual(owner1.PersonId, domainPerson3.Id);
            Assert.AreEqual(owner1.IsBeneficial, true);
            Assert.AreEqual(owner1.Name, domainPerson3.FullName);
            Assert.AreEqual(owner1.Share, 25);

            Assert.AreEqual(owner2.CompanyId, domainCompany.Id);
            Assert.AreEqual(owner2.PersonId, domainPerson4.Id);
            Assert.AreEqual(owner2.IsBeneficial, true);
            Assert.AreEqual(owner2.Name, domainPerson4.FullName);
            Assert.AreEqual(owner2.Share, 75);
        }
    }
}
