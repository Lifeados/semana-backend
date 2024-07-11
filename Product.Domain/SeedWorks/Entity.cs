namespace Product.Domain.SeedWorks;

public abstract class Entity
{
    public int Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; }
}