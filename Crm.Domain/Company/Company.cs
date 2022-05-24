namespace Crm.Domain.Company;

public class Company : Entity
{
    private readonly string name;
    private readonly Guid id;
    private HashSet<Owner> owners;

    public Company(Guid id, string name)
    {
        AssertNotEmpty(id, nameof(id));
        AssertNotEmpty(name, nameof(name));

        this.id = id;
        this.name = name;
    }

    public Guid Id => id;
    public string Name => name;
    public IReadOnlySet<Owner> Owners => owners;

    public void SetOwners(IEnumerable<SetOwnersArg> args)
    {
        if (args is null)
        {
            owners = null;
        }

        if (args.Sum(o => o.Share) != 100)
        {
            throw new Exception("Sum of Owners shares must be equal 100.");
        }

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
}
