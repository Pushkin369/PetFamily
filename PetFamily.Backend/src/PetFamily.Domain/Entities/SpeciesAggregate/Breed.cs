using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Entities.SpeciesAggregate
{
    public class Breed : Entity<Guid>
    {
        public string Name { get; private set; }
        private Breed() { } // For EF Core
        private Breed(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public static Guid NewId() => Guid.NewGuid();
        public static Result<Breed> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Breed>("Name cannot be empty");

            var breed = new Breed(NewId(), name);

            return Result.Success(breed);
        }
    }
}
