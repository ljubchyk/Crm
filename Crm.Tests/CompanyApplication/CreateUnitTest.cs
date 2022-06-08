using Crm.Application.Companies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class CreateUnitTest : UnitTestBase
    {
        [TestMethod]
        public async Task Creates()
        {
            var company = new Company
            {
                Name = "A"
            };
            await companyApplication.Create(company);

            Assert.IsNotNull(company);
            Assert.AreNotEqual(company.Id, Guid.Empty);
            Assert.AreEqual("A", company.Name);
        }

        [TestMethod]
        public async Task FailIfNameMissed()
        {
            var company = new Company();
            var action = () => companyApplication.Create(company);
            await Assert.ThrowsExceptionAsync<ArgumentException>(action);
        }
    }
}
