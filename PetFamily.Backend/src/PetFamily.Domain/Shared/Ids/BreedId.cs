namespace PetFamily.Domain.Shared.Ids;

public record BreedId(Guid Value)
{
    public static BreedId NewBreedId => new BreedId(Guid.NewGuid());
    public static BreedId Empty => new BreedId(Guid.Empty);
    public static BreedId Create(Guid id) => new BreedId(id);
}