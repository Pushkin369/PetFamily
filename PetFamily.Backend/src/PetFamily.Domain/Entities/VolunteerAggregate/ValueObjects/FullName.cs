using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record FullName(string Name, string Surname, string? Patronymic)
    {
        public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (string.IsNullOrWhiteSpace(surname))
                return Errors.General.ValidationEmpty(surname);

            if (name.Length > Constants.MAX_LENGTH_LOW_TEXT)
                return Errors.General.ValidationLength(name, $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");

            if (surname.Length > 100)
                return Errors.General.ValidationLength(surname, $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");

            if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > 100)
                return Errors.General.ValidationLength(patronymic, $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");

            return new FullName(name, surname, patronymic);
        }
    }
}