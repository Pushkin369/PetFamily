using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public sealed record Email
{
    public const int EMAIL_MAX_LENGTH = 256;

    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email, Error> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValidationEmpty(email);

        email = email.Trim();

        if (email.Length > EMAIL_MAX_LENGTH)
            return Errors.General.ValidationLength(email,
                $"more than 0 and less than {EMAIL_MAX_LENGTH}");

        if (!EmailRegex.IsMatch(email))
            return Errors.General.ValidationFormat(email,
                "name@exaple.com");

        return new Email(email);
    }

    public override string ToString() => Value;
}