using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public sealed record Email
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>("Email is required");

        email = email.Trim();

        if (email.Length > 254)
            return Result.Failure<Email>("Email is too long");

        if (!EmailRegex.IsMatch(email))
            return Result.Failure<Email>("Email format is invalid");

        return Result.Success(new Email(email));
    }

    public override string ToString() => Value;
}