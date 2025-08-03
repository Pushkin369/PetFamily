using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Entities.SpeciesAggregate
{
    public record BreedId(Guid Value)
    {
        public static BreedId NewBreedId => new BreedId(Guid.NewGuid());

        public static BreedId Empty => new BreedId(Guid.Empty);

        public static BreedId Create(Guid id) => new BreedId(id);
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

        public static Result<Breed, Error> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);
            
            if(name.Length < Constants.MAX_LENGTH_LOW_TEXT)
                return Errors.General.ValidationLength(name, $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");
            
            return new Breed(BreedId.NewBreedId, name);
        }
    }
}