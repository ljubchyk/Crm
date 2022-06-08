using Crm.Application.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.IntegrationTests.PersonApplication
{
    [TestClass]
    public class UpdateUnitTest : UnitTestBase
    {
        [TestMethod]
        public async Task Updates()
        {
            testApplication.CreateClient();

            var domainPerson = new Domain.People.Person(
                Guid.NewGuid(),
                "A",
                "B");
            await personRepository.Create(domainPerson);

            var domainCompany = new Domain.Companies.Company(
                Guid.NewGuid(),
                "A");
            domainCompany.SetOwners(new Domain.Companies.OwnerArg(
                domainPerson,
                100));
            await companyRepository.Create(domainCompany);

            var person = new Person
            {
                Id = domainPerson.Id,
                FirstName = "A`",
                LastName = "B`"
            };

            await personApplication.Update(person);
            await Task.Delay(10000);

            domainCompany = await companyRepository.Get(domainCompany.Id);

            Assert.AreEqual(1, domainCompany.Owners.Count);

            var owner = domainCompany.GetOwner(domainPerson.Id);
            Assert.IsNotNull(owner);
            Assert.AreEqual("A` B`", owner.Name);
        }
    }
}
