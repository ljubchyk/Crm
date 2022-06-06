namespace Crm.Domain.People;

public class PersonRenamed : DomainEvent
{
    public PersonRenamed(Guid id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public Guid Id { get; }
    public string FullName { get; }
}