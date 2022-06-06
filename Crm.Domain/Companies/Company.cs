namespace Crm.Domain.Companies;

public class Company : Entity
{
    private string name;
    private readonly Guid id;
    private readonly SortedSet<Owner> owners;

    public Company(Company company)
        : base(company.id)
    {
        name = company.name;
        id = company.id;
        owners = new SortedSet<Owner>(
            company.owners.Select(
                owner => new Owner(owner)));
    }

    public Company(Guid id, string name)
        : base(id)
    {
        Assert.NotEmpty(id, nameof(id));
        Assert.NotEmpty(name, nameof(name));

        this.id = id;
        this.name = name;
        owners = new SortedSet<Owner>();
    }

    public Guid Id => id;
    public string Name => name;
    public IReadOnlySet<Owner> Owners => owners;

    public void Rename(string name)
    {
        Assert.NotEmpty(name, nameof(name));
        this.name = name;
    }

    public void SetOwners(params OwnerArg[] args)
    {
        SetOwners((ICollection<OwnerArg>)args);
    }

    public void SetOwners(ICollection<OwnerArg> owners)
    {
        Assert.NotNull(owners, nameof(owners));

        if (owners.Sum(o => o.Share) != 100)
        {
            throw new Exception("Sum of Owners shares must be equal 100.");
        }

        this.owners.Clear();
        foreach (var owner in owners)
        {
            var isAdded = this.owners.Add(
                new Owner(
                    id,
                    owner.Person.Id,
                    owner.Person.FullName,
                    owner.Share));
            if (!isAdded)
            {
                throw new InvalidOperationException($"Owner with PersonId: {owner.Person.Id} is duplicated.");
            }
        }
    }

    public Owner GetOwner(Guid personId)
    {
        return owners.FirstOrDefault(owner => owner.PersonId == personId);
    }
}
