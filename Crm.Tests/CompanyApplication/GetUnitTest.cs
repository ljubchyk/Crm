using Crm.Application.Companies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class GetUnitTest : UnitTestBase
    {
        [TestMethod]
        public async Task Returns()
        {
            var domainCompany = new Domain.Companies.Company(
                Guid.NewGuid(),
                "A");
            await companyRepository.Create(domainCompany);

            var company = await companyApplication.Get(domainCompany.Id);

            Assert.IsNotNull(company);
            Assert.AreEqual(company.Id, domainCompany.Id);
            Assert.AreEqual(company.Name, domainCompany.Name);
        }

        [TestMethod]
        public async Task ReturnsNullIfCompanyMissed()
        {
            var company = await companyApplication.Get(Guid.Empty);

            Assert.IsNull(company);
        }
    }
}
