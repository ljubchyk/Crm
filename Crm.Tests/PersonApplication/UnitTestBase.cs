using Crm.Application.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crm.Tests.PersonApplication
{
    public class UnitTestBase
    {
        protected PersonRepositoryFake personRepository;
        protected PersonApplicationService personApplication;

        [TestInitialize]
        public void SetUp()
        {
            var eventStorage = new EventStoreFake();
            personRepository = new PersonRepositoryFake(eventStorage);
            personApplication = new PersonApplicationService(
                personRepository);
        }
    }
}
