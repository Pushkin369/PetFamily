using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record Characteristic(double Weigh, double Height)
    {
        public const int MAX_CHARACTERISTIC= 200;
        public static Result<Characteristic,Error> Create(double weigth, double heigth)
        {
            if (weigth <= 0 && weigth >= 200)
                 return Errors.General.ValidationLength(weigth.ToString(), $"more than 0 and less than {MAX_CHARACTERISTIC}");

            if (heigth <= 0 && heigth >= 200)
                 return Errors.General.ValidationLength(heigth.ToString(), $"more than 0 and less than {MAX_CHARACTERISTIC}");

            return new Characteristic(weigth, heigth);
        }
    }
}
