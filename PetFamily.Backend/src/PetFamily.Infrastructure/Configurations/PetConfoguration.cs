using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities.VolunteerAggregate;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfoguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        throw new NotImplementedException();
    }
}