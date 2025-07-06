using CSharpFunctionalExtensions;
using PetFamily.Domain.PetAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.SpeciesAggregate
{
    public class Species : Entity<Guid>
    {
        private readonly List<Breed> _breed = [];


        public string Name { get; private set; }
        public IReadOnlyList<Breed> Breeds => _breed;


        private Species(Guid id, string name) : base(id)
        {
            Name = name;
        }
        private Species() { } // For EF Core


        public static Guid NewId() => Guid.NewGuid();


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

            var species = new Species(NewId(), name);

            return Result.Success(species);
        }
    }
}
