namespace Core.Entities.Common;

public abstract class Entity
{
}

public abstract class Entity<TKey> : Entity
{
    public TKey Id { get; set; }
}
