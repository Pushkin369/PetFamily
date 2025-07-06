using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public class Address : ValueObject
    {
        public string House { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }


        private Address(string house, string street, string city, string state, string country)
        {
            House = house;
            Street = street;
            City = city;
            State = state;
            Country = country;
        }

        public static Result<Address> Create(string house, string street, string city, string state, string country)
        {
            if (string.IsNullOrWhiteSpace(house))
                return Result.Failure<Address>("House is required");

            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<Address>("Street is required");

            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<Address>("City is required");

            if (string.IsNullOrWhiteSpace(state))
                return Result.Failure<Address>("State is required");

            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<Address>("Country is required");

            return Result.Success(new Address(house, street, city, state, country));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return House;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
        }
    }
}