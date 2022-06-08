using System.Collections;

namespace Crm.Domain.Companies;

public class Owner : Entity, IComparable<Owner>
{
    private readonly Guid companyId;
    private readonly Guid personId;
    private string name;
    private readonly double share;
    private readonly bool isBeneficial;

    public Owner(Owner owner)
        : base(owner.companyId, owner.personId)
    {
        companyId = owner.companyId;
        personId = owner.personId;
        name = owner.name;
        share = owner.share;
        isBeneficial = owner.isBeneficial;
    }

    public Owner(Guid companyId, Guid personId, string name, double share)
         : base(companyId, personId)
    {
        Assert.NotEmpty(companyId, nameof(companyId));
        Assert.NotEmpty(personId, nameof(personId));
        Assert.NotEmpty(name, nameof(name));
        Assert.GreaterThanZero(share, nameof(share));

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

    internal void Rename(string name)
    {
        this.name = name;
    }

    public int CompareTo(Owner other)
    {
        Assert.NotNull(other, nameof(other));
        return name.CompareTo(other.name);
    }
}