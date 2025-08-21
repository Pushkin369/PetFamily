namespace PetFamily.Domain.Shared.Ids;

public record VolunteerId(Guid Value)
{
    public static VolunteerId NewVolunteerId => new VolunteerId(Guid.NewGuid());
    public static VolunteerId Empty => new VolunteerId(Guid.Empty);
    public static VolunteerId Create(Guid id) => new VolunteerId(id);
}