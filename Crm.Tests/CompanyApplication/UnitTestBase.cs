using Crm.Application.Companies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crm.Tests.CompanyApplication
{
    public class UnitTestBase
    {
        protected PersonRepositoryFake personRepository;
        protected CompanyRepositoryFake companyRepository;
        protected CompanyApplicationService companyApplication;
        protected EventStoreFake eventStorageFake;

        [TestInitialize]
        public void SetUp()
        {
            eventStorageFake = new EventStoreFake();
            personRepository = new PersonRepositoryFake(eventStorageFake);
            companyRepository = new CompanyRepositoryFake(eventStorageFake);
            companyApplication = new CompanyApplicationService(
                companyRepository,
                personRepository);
        }
    }
}
