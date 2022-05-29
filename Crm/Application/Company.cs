namespace Crm.Application
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Company Create(Domain.Company.Company company)
        {
            return new Company
            {
                Id = company.Id,
                Name = company.Name
            };
        }
    }
}
