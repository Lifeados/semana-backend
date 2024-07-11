using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Models.EstablishmentAggregate.Entities;

namespace Product.Data.Mappings;

public class EstablishmentMapping : IEntityTypeConfiguration<Establishment>
{
    public void Configure(EntityTypeBuilder<Establishment> builder)
    {
        builder.ToTable("Establishment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.IsActive);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);
    }
}