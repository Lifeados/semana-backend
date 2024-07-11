namespace Product.Api.Controllers.V1.Dtos;

public record UpdateProductDto(
    string Code,
    string Name,
    string Description,
    bool IsSignature
);