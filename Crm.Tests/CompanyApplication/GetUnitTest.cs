using Crm.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Crm.Tests.CompanyApplication
{
    [TestClass]
    public class GetUnitTest
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
