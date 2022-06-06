namespace Crm.Domain;

public abstract class Entity : IEquatable<Entity>
{
    private readonly Guid[] id;
    private readonly List<DomainEvent> domainEvents;

    private Entity()
    {
        domainEvents = new List<DomainEvent>();
    }

    protected Entity(Guid id)
        : this()
    {
        this.id = new[] { id };
    }

    protected Entity(Guid id1, Guid id2)
        : this()
    {
        id = new[] { id1, id2 };
    }

    public IReadOnlyList<DomainEvent> DomainEvents => 
        domainEvents;

    public bool Equals(Entity other)
    {
        if (other is null)
        {
            return false;
        }

        return id.SequenceEqual(other.id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity);
    }

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}
