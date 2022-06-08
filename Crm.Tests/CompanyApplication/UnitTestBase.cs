using Crm.Application.Companies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crm.Tests.CompanyApplication
{
    public class UnitTestBase
    {
        protected PersonRepositoryFake personRepository;
        protected CompanyRepositoryFake companyRepository;
        protected CompanyApplicationService companyApplication;
        protected EventStorageFake eventStorageFake;

        [TestInitialize]
        public void SetUp()
        {
            eventStorageFake = new EventStorageFake();
            personRepository = new PersonRepositoryFake(eventStorageFake);
            companyRepository = new CompanyRepositoryFake(eventStorageFake);
            companyApplication = new CompanyApplicationService(
                companyRepository,
                personRepository);
        }
    }
}
