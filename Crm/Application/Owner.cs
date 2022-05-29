namespace Crm.Application
{
    public class Owner
    {
        public Guid CompanyId { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public double Share { get; set; }
        public bool IsBeneficial { get; set; }
    }
}
