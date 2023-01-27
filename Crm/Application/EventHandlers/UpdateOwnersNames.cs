//using Crm.Domain.Companies;
//using Crm.Domain.People;

//namespace Crm.Application.EventHandlers
//{
//    public class UpdateOwnersNames
//    {
//        private readonly ICompanyRepository companyRepository;

//        public UpdateOwnersNames(ICompanyRepository companyRepository)
//        {
//            this.companyRepository = companyRepository;
//        }

//        public async Task Execute()
//        {
//            var companies = await companyRepository.GetListWithOwner(context.Message.Id);
//            foreach (var company in companies)
//            {
//                company.RenameOwner(
//                    context.Message.Id,
//                    context.Message.FullName);
//            }
//            await companyRepository.Update(companies);
//        }
//    }
//}
