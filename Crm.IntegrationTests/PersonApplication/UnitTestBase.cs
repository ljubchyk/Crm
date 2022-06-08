using Crm.Application.People;
using Crm.Domain.Companies;
using Crm.Domain.People;
using Crm.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Crm.IntegrationTests.PersonApplication
{
    [TestClass]
    public class UnitTestBase
    {
        protected IPersonRepository personRepository;
        protected ICompanyRepository companyRepository;
        protected PersonApplicationService personApplication;
        protected static TestApplication testApplication;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            testApplication = new TestApplication();
        }

        [TestInitialize]
        public void TestSetUp()
        {
            personRepository = testApplication.Services.GetRequiredService<IPersonRepository>();
            companyRepository = testApplication.Services.GetRequiredService<ICompanyRepository>();
            personApplication = new PersonApplicationService(
                personRepository);
        }
    }
}
