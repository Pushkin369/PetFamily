using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record Characteristic(double Weigh, double Height)
    {
        public static Result<Characteristic> Create(double weigth, double heigth)
        {
            if (weigth <= 0 && weigth >= 200)
                return Result.Failure<Characteristic>("Weight must be greater than 0 and less 200");

            if (heigth <= 0 && heigth >= 200)
                return Result.Failure<Characteristic>("Height must be greater than 0 and less 200");

            return Result.Success(new Characteristic(weigth, heigth));
        }
    }
}
