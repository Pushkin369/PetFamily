using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Entities.SpeciesAggregate
{
    public record BreedId(Guid Value)
    {
        public static BreedId NewBreedId => new BreedId(Guid.NewGuid());

        public static BreedId Empty => new BreedId(Guid.Empty);
    }


    public class Breed : Shared.Entity<BreedId>
    {
        public string Name { get; private set; }

        private Breed(BreedId id) : base(id)
        {
        } // For EF Core

        private Breed(BreedId breedId, string name) : base(breedId)
        {
            Name = name;
        }

        public static Result<Breed> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Breed>("Name cannot be empty");

            var breed = new Breed(BreedId.NewBreedId, name);

            return Result.Success(breed);
        }
    }
}