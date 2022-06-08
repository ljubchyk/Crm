using Crm.Application.Companies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class GetListUnitTest : UnitTestBase
    {
        [TestMethod]
        public async Task ReturnsList()
        {
            var domainCompany1 = new Domain.Companies.Company(
                Guid.NewGuid(),
                "A");
            var domainCompany2 = new Domain.Companies.Company(
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
    }
}
