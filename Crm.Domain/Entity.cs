namespace Crm.Domain;

public abstract class Entity : IEquatable<Entity>
{
    private readonly Guid[] id;

    private Entity()
    {

    }

    protected Entity(Guid id)
    {
        this.id = new[] { id };
    }

    protected Entity(Guid id1, Guid id2)
    {
        id = new[] { id1, id2 };
    }

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
}
