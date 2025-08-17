using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Domain.Entities.SpeciesAggregate
{
    public class Breed : Shared.Entity<BreedId>
    {
        public string Name { get; private set; }

        // For EF Core
        private Breed(BreedId id) : base(id)
        {
        }

        private Breed(BreedId breedId, string name) : base(breedId)
        {
            Name = name;
        }

        public static Result<Breed, Error> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (name.Length < Constants.MAX_LENGTH_LOW_TEXT)
                return Errors.General.ValidationLength(name,
                    $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");

            return new Breed(BreedId.NewBreedId, name);
        }
    }
}