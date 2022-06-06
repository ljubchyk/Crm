namespace Crm.Application
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Company Create(Domain.Companies.Company company)
        {
            var result = new Company();
            Fill(result, company);
            return result;
        }

        public static void Fill(Company target, Domain.Companies.Company company)
        {
            target.Id = company.Id;
            target.Name = company.Name;
        }
    }
}
