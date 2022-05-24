using Crm.Domain.Company;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var companies = await companyRepository.GetList();
            return Ok(companies);
        }

        [HttpGet("{id}/owners")]
        public async Task<IActionResult> GetOwners(Guid id)
        {
            var company = await companyRepository.Get(id);
            if (company is null)
            {
                return NotFound();
            }

            return Ok(company.Owners);
        }
    }
}