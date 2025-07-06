using CSharpFunctionalExtensions;

namespace PetFamily.Domain.PetAggregate
{
    public class RequisitesValueObject
    {
        public string Name { get; }
        public string Description { get; }
        public string DescriptionTransfer { get; }

        private RequisitesValueObject(string name, string description, string descriptionTransfer)
        {
            Name = name;
            Description = description;
            DescriptionTransfer = descriptionTransfer;
        }

        public static Result<RequisitesValueObject> Create(string name, string description, string descriptionTransfer)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<RequisitesValueObject>("Name is required");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<RequisitesValueObject>("Description is required");

            if (string.IsNullOrWhiteSpace(descriptionTransfer))
                return Result.Failure<RequisitesValueObject>("Description for transfer is required");

            return Result.Success(new RequisitesValueObject(name, description, descriptionTransfer));
        }
    }
}

