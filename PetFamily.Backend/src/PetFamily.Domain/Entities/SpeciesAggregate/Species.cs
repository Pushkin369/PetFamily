using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Entities.SpeciesAggregate
{
    public record SpeciesId(Guid Value)
    {
        public static SpeciesId NewSpeciesId => new SpeciesId(Guid.NewGuid());

        public static SpeciesId Empty => new SpeciesId(Guid.Empty);

        public static SpeciesId Create(Guid id) => new SpeciesId(id);
    }


    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breed = [];
        public string Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _breed;

        private Species(SpeciesId speciesId) : base(speciesId)
        {
        } // For EF Core

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

        public static Result<Species> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Species>("Name cannot be empty");

            var species = new Species(SpeciesId.NewSpeciesId, name);

            return Result.Success(species);
        }
    }
}