using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record Requisites(string Name, string Description, string DescriptionTransfer)
    {
        public static Result<Requisites, Error> Create(string name, string description, string descriptionTransfer)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (string.IsNullOrWhiteSpace(description))
                return Errors.General.ValidationEmpty(description);

            if (string.IsNullOrWhiteSpace(descriptionTransfer))
                return Errors.General.ValidationEmpty(descriptionTransfer);

            return new Requisites(name, description, descriptionTransfer);
        }
    }
}