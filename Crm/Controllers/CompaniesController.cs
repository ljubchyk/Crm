using Crm.Application;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyApplicationService companyApplication;

        public CompaniesController(ICompanyApplicationService companyApplication)
        {
            this.companyApplication = companyApplication ?? throw new ArgumentNullException(nameof(companyApplication));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var company = await companyApplication.Get(id);
            if (company is null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var companies = await companyApplication.GetList();
            return Ok(companies);
        }

        [HttpGet("{id}/owners")]
        public async Task<IActionResult> GetOwners(Guid id)
        {
            try
            {
                var owners = await companyApplication.GetOwners(id);
                return Ok(owners);
            }
            catch (EntityDoesntExistException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/owners")]
        public async Task<IActionResult> UpdateOwners(Guid id, IEnumerable<Owner> owners)
        {
            await companyApplication.UpdateOwners(id, owners);
            return Ok(owners);
        }
    }
}