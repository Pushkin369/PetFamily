namespace PetFamily.Domain.Shared.Ids;

public record PetId(Guid Value)
{
    public static PetId NewPetId => new PetId(Guid.NewGuid());
    public static PetId Empty => new PetId(Guid.Empty);
    public static PetId Create(Guid id) => new PetId(id);
}