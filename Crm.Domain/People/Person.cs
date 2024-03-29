﻿namespace Crm.Domain.People;

public class Person : Entity
{
    private string fullName;
    private string firstName;
    private string lastName;
    private Address address;
    private readonly Guid id;

    public Person(Person person)
        : base(person.id)
    {
        fullName = person.fullName;
        firstName = person.firstName;
        lastName = person.lastName;
        address = person.address;
        id = person.id;
    }

    public Person(Guid id, string firstName, string lastName, Address address = null)
        : base(id)
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
        AddDomainEvent(new PersonRenamed(id, fullName));
    }

    public void Relocate(Address address)
    {
        SetAddress(address);
    }

    private void SetName(string firstName, string lastName)
    {
        Assert.NotEmpty(firstName, nameof(firstName));
        Assert.NotEmpty(lastName, nameof(lastName));

        this.firstName = firstName;
        this.lastName = lastName;

        fullName = $"{firstName} {lastName}";
    }

    private void SetAddress(Address address)
    {
        this.address = address;
    }
}