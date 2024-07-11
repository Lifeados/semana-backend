using Product.Domain.SeedWorks;

namespace Product.Domain.Models.EstablishmentAggregate.Entities;

public class Establishment(string name) : AggregateRoot
{
    public string Name { get; private set; } = name;
    public bool IsActive { get; private set; } = true;

    public void Update(string name)
    {
        Name = name;
    }
}