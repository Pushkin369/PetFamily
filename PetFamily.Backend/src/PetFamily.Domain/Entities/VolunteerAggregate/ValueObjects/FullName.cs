using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record FullName(string Name, string Surname, string? Patronymic)
    {
        public static Result<FullName> Create(string name, string surname, string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<FullName>("Name is required");

            if (string.IsNullOrWhiteSpace(surname))
                return Result.Failure<FullName>("Surname is required");

            if (name.Length > 100)
                return Result.Failure<FullName>("Name is too long");

            if (surname.Length > 100)
                return Result.Failure<FullName>("Surname is too long");

            if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > 100)
                return Result.Failure<FullName>("Patronymic is too long");

            return Result.Success(new FullName(name, surname, patronymic));
        }
    }
}
