using CSharpFunctionalExtensions;

namespace PetFamily.Domain.VolunteerAggregate
{
    public class FIOValueObject
    {
        public string Name { get; }
        public string Surname { get; }
        public string? Patronymic { get; }



        private FIOValueObject(string name, string surname, string? patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public static Result<FIOValueObject> Create(string name, string surname, string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<FIOValueObject>("Name is required");

            if (string.IsNullOrWhiteSpace(surname))
                return Result.Failure<FIOValueObject>("Surname is required");

            if (name.Length > 100)
                return Result.Failure<FIOValueObject>("Name is too long");

            if (surname.Length > 100)
                return Result.Failure<FIOValueObject>("Surname is too long");

            if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > 100)
                return Result.Failure<FIOValueObject>("Patronymic is too long");

            return Result.Success(new FIOValueObject(name, surname, patronymic));
        }
    }
}
