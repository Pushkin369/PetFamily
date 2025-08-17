using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities.VolunteerAggregate;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest? request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = Volunteer.Create(
            request.firstname,
            request.surname,
            request.patronymic,
            request.phone,
            request.email,
            request.nameReq,
            request.descriptionReq,
            request.descriptionTransferReq,
            request.description,
            request.experience);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        volunteerResult.Value.AddSocialNetwork("test",
            "https://chatgpt.com");

        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id.Value;
    }
}