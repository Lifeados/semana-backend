using Product.Domain.SeedWorks;

namespace Product.Domain.Models.ProductAggregate.Entities;

public class Product(int establishmentId , string code, string name, string description, bool isSignature) : AggregateRoot
{
    public int EstablishmentId { get; private set; } = establishmentId;
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public bool IsSignature { get; private set; } = isSignature;
    public bool IsActive { get; private set; } = true;

    public void Update(string code, string name, string description, bool isSignature)
    {
        Code = code;
        Name = name;
        Description = description;
        IsSignature = isSignature;
    }
}