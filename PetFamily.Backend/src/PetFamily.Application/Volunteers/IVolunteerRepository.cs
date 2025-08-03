using PetFamily.Domain.Entities.VolunteerAggregate;

namespace PetFamily.Application.Volunteers;

public interface IVolunteerRepository
{
    public Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
}