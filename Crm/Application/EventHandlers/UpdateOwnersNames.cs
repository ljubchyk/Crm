using Crm.Domain.Companies;
using Crm.Domain.People;
using MassTransit;

namespace Crm.Application.EventHandlers
{
    public class UpdateOwnersNames : IConsumer<PersonRenamed>
    {
        private readonly ICompanyRepository companyRepository;

        public UpdateOwnersNames(/*ICompanyRepository companyRepository*/)
        {
            companyRepository = null;
        }

        public async Task Consume(ConsumeContext<PersonRenamed> context)
        {
            var companies = await companyRepository.GetListWithOwner(context.Message.Id);
            foreach (var company in companies)
            {
                company.RenameOwner(
                    context.Message.Id,
                    context.Message.FullName);
            }
            await companyRepository.Update(companies);
        }
    }
}
