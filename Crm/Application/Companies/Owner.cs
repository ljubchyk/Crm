namespace Crm.Application.Companies
{
    public class Owner
    {
        public Guid CompanyId { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public double Share { get; set; }
        public bool IsBeneficial { get; set; }

        public static Owner Create(Domain.Companies.Owner owner)
        {
            var result = new Owner();
            Fill(result, owner);
            return result;
        }

        public static void Fill(Owner target, Domain.Companies.Owner owner)
        {
            target.CompanyId = owner.CompanyId;
            target.IsBeneficial = owner.IsBeneficial;
            target.Name = owner.Name;
            target.PersonId = owner.PersonId;
            target.Share = owner.Share;
        }
    }
}
