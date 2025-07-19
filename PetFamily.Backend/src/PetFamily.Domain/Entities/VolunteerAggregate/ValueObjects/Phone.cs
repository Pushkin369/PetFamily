using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

// или замени на свою Result-структуру

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public record Phone(string Value)
{
    private static readonly Regex PhoneRegex = new(@"^\+?\d{10,15}$", RegexOptions.Compiled);

    public static Result<Phone> Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return Result.Failure<Phone>("Phone number is required");

        phone = phone.Trim();

        if (!PhoneRegex.IsMatch(phone))
            return Result.Failure<Phone>("Invalid phone number format");

        return Result.Success(new Phone(phone));
    }
}