using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record Requisites(string Name, string Description, string DescriptionTransfer)
    {
        public static Result<Requisites> Create(string name, string description, string descriptionTransfer)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Requisites>("Name is required");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Requisites>("Description is required");

            if (string.IsNullOrWhiteSpace(descriptionTransfer))
                return Result.Failure<Requisites>("Description for transfer is required");

            return Result.Success(new Requisites(name, description, descriptionTransfer));
        }
    }
}

