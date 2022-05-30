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
            domainCompany.SetOwners(
                new Domain.Company.SetOwnersArg(domainPerson1, 50),
                new Domain.Company.SetOwnersArg(domainPerson2, 50));

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
                owner1,
                owner2);

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

            domainCompany = await companyRepository.Get(domainCompany.Id);

            var domainOwner1 = domainCompany.GetOwner(domainPerson3.Id);
            Assert.IsNotNull(domainOwner1);
            Assert.AreEqual(domainOwner1.CompanyId, domainCompany.Id);
            Assert.AreEqual(domainOwner1.PersonId, domainPerson3.Id);
            Assert.AreEqual(domainOwner1.IsBeneficial, true);
            Assert.AreEqual(domainOwner1.Name, domainPerson3.FullName);
            Assert.AreEqual(domainOwner1.Share, 25);

            var domainOwner2 = domainCompany.GetOwner(domainPerson4.Id);
            Assert.IsNotNull(domainOwner2);
            Assert.AreEqual(domainOwner2.CompanyId, domainCompany.Id);
            Assert.AreEqual(domainOwner2.PersonId, domainPerson4.Id);
            Assert.AreEqual(domainOwner2.IsBeneficial, true);
            Assert.AreEqual(domainOwner2.Name, domainPerson4.FullName);
            Assert.AreEqual(domainOwner2.Share, 75);
        }

        [TestMethod]
        public async Task FailsIfMissedPerson()
        {
            var domainCompany = new Domain.Company.Company(
                Guid.NewGuid(),
                "A");

            await companyRepository.Create(domainCompany);

            var domainPerson = new Domain.People.Person(
                Guid.NewGuid(),
                "A3",
                "B3");

            await personRepository.Create(domainPerson);

            var action = () => companyApplication.UpdateOwners(
                domainCompany.Id,
                new Owner
                {
                    PersonId = Guid.Empty
                });
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                action);
        }

        [TestMethod]
        public async Task FailsIfMissedCompany()
        {
            var action = () => companyApplication.UpdateOwners(
                Guid.Empty);
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                action);
        }
    }
}
