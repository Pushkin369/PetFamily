using CSharpFunctionalExtensions;

namespace PetFamily.Domain.PetAggregate
{
    public class AddressValueObject
    {
        public string House { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }


        private AddressValueObject(string house, string street, string city, string state, string country)
        {
            House = house;
            Street = street;
            City = city;
            State = state;
            Country = country;
        }

        public static Result<AddressValueObject> Create(string house, string street, string city, string state, string country)
        {
            if (string.IsNullOrWhiteSpace(house))
                return Result.Failure<AddressValueObject>("House is required");

            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<AddressValueObject>("Street is required");

            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<AddressValueObject>("City is required");

            if (string.IsNullOrWhiteSpace(state))
                return Result.Failure<AddressValueObject>("State is required");

            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<AddressValueObject>("Country is required");

            return Result.Success(new AddressValueObject(house, street, city, state, country));
        }
    }
}