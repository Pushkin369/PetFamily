using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;

public record Address(string Country, string State, string City, string Street, string House)
{
    public static Result<Address,Error> Create(
        string country, 
        string state, 
        string city, 
        string street, 
        string house)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValidationEmpty(country);

        if (string.IsNullOrWhiteSpace(state))
            return Errors.General.ValidationEmpty(state);

        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValidationEmpty(city);

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValidationEmpty(street);

        if (string.IsNullOrWhiteSpace(house))
            return Errors.General.ValidationEmpty(house);

        return new Address(country, state, city, street, house);
    }
}