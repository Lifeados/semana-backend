namespace Product.Domain.Models.ProductAggregate.Queries.Dtos;

public class GetAllProductDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsSignature { get; set; }
}