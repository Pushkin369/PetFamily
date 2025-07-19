using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public record Address(string Country, string State, string City, string Street, string House)
{
    public static Result<Address> Create(string country, string state, string city, string street, string house)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Result.Failure<Address>("Country is required");

        if (string.IsNullOrWhiteSpace(state))
            return Result.Failure<Address>("State is required");

        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>("City is required");

        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>("Street is required");

        if (string.IsNullOrWhiteSpace(house))
            return Result.Failure<Address>("House is required");

        return Result.Success(new Address(country, state, city, street, house));
    }
}