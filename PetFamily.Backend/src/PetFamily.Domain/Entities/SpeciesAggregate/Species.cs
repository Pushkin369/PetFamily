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
    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breed = [];
        public string Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _breed;

        // For EF Core
        private Species() : base(default!)
        {
        }

        private Species(SpeciesId speciesId, string name) : base(speciesId)
        {
            Name = name;
        }

        public Result AddPet(Breed? breed)
        {
            if (breed is null)
                return Result.Failure("Breed cannot be null to add");

            _breed.Add(breed);

            return Result.Success();
        }

        public static Result<Species, Error> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (name.Length < Constants.MAX_LENGTH_LOW_TEXT)
                return Errors.General.ValidationLength(name,
                    $"more than 0 and less than {Constants.MAX_LENGTH_LOW_TEXT}");

            return new Species(SpeciesId.NewSpeciesId, name);
        }
    }
}