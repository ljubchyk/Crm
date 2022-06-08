using Crm.Application.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.PersonApplication
{
    [TestClass]
    public class UpdateUnitTest : UnitTestBase
    {
        [TestMethod]
        public async Task Updates()
        {
            var domainPerson = new Domain.People.Person(
                Guid.NewGuid(),
                "A",
                "B");
            await personRepository.Create(domainPerson);

            var person = new Person
            {
                Id = domainPerson.Id,
                FirstName = "A`",
                LastName = "B`"
            };

            await personApplication.Update(person);

            Assert.AreEqual("A`", person.FirstName);
            Assert.AreEqual("B`", person.LastName);
            Assert.AreEqual("A` B`", person.FullName);
        }
    }
}
