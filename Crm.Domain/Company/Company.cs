namespace Crm.Domain.Company;

public class Company : Entity
{
    private readonly string name;
    private readonly Guid id;
    private HashSet<Owner> owners;

    public Company(Company company)
    {
        name = company.name;
        id = company.id;
        owners = new HashSet<Owner>(
            company.owners.Select(
                owner => new Owner(owner)));
    }
    
    public Company(Guid id, string name)
    {
        AssertNotEmpty(id, nameof(id));
        AssertNotEmpty(name, nameof(name));

        this.id = id;
        this.name = name;
        owners = new HashSet<Owner>();
    }

    public Guid Id => id;
    public string Name => name;
    public IReadOnlySet<Owner> Owners => owners;

    public void SetOwners(params SetOwnersArg[] args)
    {
        SetOwners((IEnumerable<SetOwnersArg>)args);
    }

    public void SetOwners(IEnumerable<SetOwnersArg> args)
    {
        AssertNotNull(args, "owners");

        if (args.Sum(o => o.Share) != 100)
        {
            throw new Exception("Sum of Owners shares must be equal 100.");
        }

        owners.Clear();
        foreach (var arg in args)
        {
            owners.Add(
                new Owner(
                    id,
                    arg.Person.Id,
                    arg.Person.FullName,
                    arg.Share));
        }
    }

    public Owner GetOwner(Guid personId)
    {
        return owners.FirstOrDefault(owner => owner.PersonId == personId);
    }
}
