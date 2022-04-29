namespace Crm.Domain.Person;

public class Person : Entity
{
    private string fullName = string.Empty;
    private string firstName = string.Empty;
    private string lastName = string.Empty;
    private Address? address;

    private Person()
    {
    }

    public Person(string firstName, string lastName, Address? address = null)
    {
        SetName(firstName, lastName);
        SetAddress(address);
    }

    public string FirstName => firstName;
    public string LastName => lastName;
    public string FullName => fullName;
    public Address? Address => address;

    public void Rename(string firstName, string lastName)
    {
        SetName(firstName, lastName);
    }

    public void Relocate(Address? address)
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

    private void SetAddress(Address? address)
    {
        this.address = address;
    }
}
