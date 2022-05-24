namespace Crm.Domain.Company;

public class Owner : Entity
{
    private readonly Guid companyId;
    private readonly Guid personId;
    private readonly string name;
    private readonly double share;
    private readonly bool isBeneficial;

    public Owner(Guid companyId, Guid personId, string name, double share)
    {
        AssertNotEmpty(companyId, nameof(companyId));
        AssertNotEmpty(personId, nameof(personId));
        AssertNotEmpty(name, nameof(name));
        AssertGreaterThanZero(share, nameof(share));

        this.companyId = companyId;
        this.personId = personId;
        this.name = name;
        this.share = share;
        isBeneficial = share >= 25;
    }

    public Guid CompanyId => companyId;
    public Guid PersonId => personId;
    public string Name => name;
    public double Share => share;
    public bool IsBeneficial => isBeneficial;

    public override bool Equals(object obj)
    {
        return obj is Owner owner &&
               companyId.Equals(owner.companyId) &&
               personId.Equals(owner.personId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(companyId, personId);
    }
}
