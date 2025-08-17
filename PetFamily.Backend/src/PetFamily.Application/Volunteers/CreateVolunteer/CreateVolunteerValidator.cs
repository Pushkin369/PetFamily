using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => new { c.firstname, c.surname, c.patronymic })
            .MustBeValueObject(fio => FullName.Create(fio.firstname, fio.surname, fio.patronymic));
        
        RuleFor(c => c.email).MustBeValueObject(Email.Create);
        RuleFor(c => c.phone).MustBeValueObject(Phone.Create);
        
        RuleFor(c => new { c.nameReq, c.descriptionReq, c.descriptionTransferReq })
            .MustBeValueObject(fio => Requisites.Create(fio.nameReq, fio.descriptionReq, fio.descriptionTransferReq));
    } 
}