using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities.SpeciesAggregate;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);
        
        builder.HasMany(x=>x.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}