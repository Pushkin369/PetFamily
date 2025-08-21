namespace PetFamily.Domain.Shared.Ids;

public record SpeciesId(Guid Value)
{
    public static SpeciesId NewSpeciesId => new SpeciesId(Guid.NewGuid());
    public static SpeciesId Empty => new SpeciesId(Guid.Empty);
    public static SpeciesId Create(Guid id) => new SpeciesId(id);
}