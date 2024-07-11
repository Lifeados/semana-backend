namespace Product.Api.Controllers.V1.Dtos;

public record CreateProductDto(
    string Code,
    string Name,
    string Description,
    bool IsSignature
);