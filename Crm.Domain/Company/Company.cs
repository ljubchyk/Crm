namespace Crm.Domain.Company;

public class Company : Entity
{
    private readonly string name;
    private readonly Guid id;
    private readonly SortedSet<Owner> owners;

    public Company(Company company)
    {
        name = company.name;
        id = company.id;
        owners = new SortedSet<Owner>(
            company.owners.Select(
                owner => new Owner(owner)),
            new OwnerComparer());
    }
    
    public Company(Guid id, string name)
    {
        Assert.NotEmpty(id, nameof(id));
        Assert.NotEmpty(name, nameof(name));

        this.id = id;
        this.name = name;
        owners = new SortedSet<Owner>(new OwnerComparer());
    }

    public Guid Id => id;
    public string Name => name;
    public IReadOnlySet<Owner> Owners => owners;

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

    private class OwnerComparer : IComparer<Owner>
    {
        public int Compare(Owner x, Owner y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
