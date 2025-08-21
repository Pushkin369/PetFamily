using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities.VolunteerAggregate;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;
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
        var fullName = FullName.Create(
            request.firstname,
            request.surname,
            request.patronymic).Value;

        var phone = Phone.Create(request.phone).Value;

        var email = Email.Create(request.email).Value;

        var req = Requisites.Create(
            request.nameReq,
            request.descriptionReq,
            request.descriptionTransferReq).Value;

        var volunteerResult = Volunteer.Create(fullName, phone, email, req,request.description, request.experience);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        volunteerResult.Value.AddSocialNetwork("test",
            "https://chatgpt.com");

        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id.Value;
    }
}