using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

// или замени на свою Result-структуру

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public record Phone(string Value)
{
    private static readonly Regex PhoneRegex = new(@"^\+?\d{10,15}$", RegexOptions.Compiled);

    public static Result<Phone, Error> Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return Errors.General.ValidationEmpty(phone);

        phone = phone.Trim();

        if (!PhoneRegex.IsMatch(phone))
            return Errors.General.ValidationFormat(phone, 
                "+7(999)999-99-99");

        return new Phone(phone);
    }
}