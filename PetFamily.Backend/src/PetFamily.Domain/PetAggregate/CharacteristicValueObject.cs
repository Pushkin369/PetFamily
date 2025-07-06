using CSharpFunctionalExtensions;

namespace PetFamily.Domain.PetAggregate
{
    public class CharacteristicValueObject
    {
        public double Weigth { get; }
        public double Heigth { get; }


        private CharacteristicValueObject(double weigth, double heigth)
        {
            Weigth = weigth;
            Heigth = heigth;
        }

        public static Result<CharacteristicValueObject> Create(double weigth, double heigth)
        {
            if (weigth <= 0 && weigth >= 200)
                return Result.Failure<CharacteristicValueObject>("Weight must be greater than 0 and less 200");

            if (heigth <= 0 && heigth >= 200)
                return Result.Failure<CharacteristicValueObject>("Height must be greater than 0 and less 200");

            return Result.Success(new CharacteristicValueObject(weigth, heigth));
        }
    }
}
