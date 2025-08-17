namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValidationEmpty(string nameOfEntity)
            => Error.ValidationError("value.is.invalid",
                $"VALIDATION ERROR: parameter {nameOfEntity} cannot be empty");

        public static Error ValidationLength(string nameOfEntity, string lengthLimit)
            => Error.ValidationError("value.is.invalid",
                $"VALIDATION ERROR: parameter length should be {lengthLimit}.");

        public static Error ValidationFormat(string nameOfEntity, string format)
            => Error.ValidationError("value.is.invalid",
                $"VALIDATION ERROR: format of {nameOfEntity} is invalid");
    }
}