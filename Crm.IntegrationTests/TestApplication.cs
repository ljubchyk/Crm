using Crm.Domain.Companies;
using Crm.Domain.People;
using Crm.Infrastructure;
using Crm.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Crm.IntegrationTests
{
    public class TestApplication : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IEventStore, EventStoreFake>();
                services.AddSingleton<IPersonRepository, PersonRepositoryFake>();
                services.AddSingleton<ICompanyRepository, CompanyRepositoryFake>();
            });
        }
    }
}