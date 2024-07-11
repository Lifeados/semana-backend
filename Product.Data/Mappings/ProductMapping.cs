using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Domain.Models.ProductAggregate.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Models.ProductAggregate.Entities.Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EstablishmentId);

        builder.Property(x => x.Code)
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR(255)");
        
        builder.Property(x => x.Description)
            .HasColumnType("VARCHAR(255)");
        
        builder.Property(x => x.IsActive);

        builder.Property(x => x.CreatedAt);
        
        builder.Property(x => x.UpdatedAt);
    }
}