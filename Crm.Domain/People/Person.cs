namespace Crm.Domain.People;

public class Person : Entity
{
    private string fullName;
    private string firstName;
    private string lastName;
    private Address address;
    private readonly Guid id;

    private Person()
    {
    }

    public Person(Guid id, string firstName, string lastName, Address address = null)
    {
        SetName(firstName, lastName);
        SetAddress(address);
        this.id = id;
    }

    public string FirstName => firstName;
    public string LastName => lastName;
    public string FullName => fullName;
    public Address Address => address;
    public Guid Id => id;

    public void Rename(string firstName, string lastName)
    {
        SetName(firstName, lastName);
    }

    public void Relocate(Address address)
    {
        SetAddress(address);
    }

    private void SetName(string firstName, string lastName)
    {
        AssertNotEmpty(firstName, nameof(firstName));
        AssertNotEmpty(lastName, nameof(lastName));

        this.firstName = firstName;
        this.lastName = lastName;

        fullName = $"{firstName} {lastName}";
    }

    private void SetAddress(Address address)
    {
        this.address = address;
    }
}
