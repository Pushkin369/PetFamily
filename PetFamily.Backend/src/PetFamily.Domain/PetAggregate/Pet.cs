using CSharpFunctionalExtensions;
using PetFamily.Domain.SpeciesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetFamily.Domain.PetAggregate
{

    public class Pet : Entity<Guid>
    {
        public string Nickname { get; private set; }
        public Guid SpeciesId { get; private set; }
        public Guid BreedId { get; private set; }
        public string? Color { get; private set; }
        public string? GeneralDescription { get; private set; }
        public string? InfoAboutHealth { get; private set; }
        public string OwnerPhoneNumber { get; private set; }
        public AddressValueObject Address { get; private set; }
        public CharacteristicValueObject Characteristic { get; private set; }
        public RequisitesValueObject Requisites { get; private set; }
        public bool IsVaccinated { get; private set; }
        public bool IsCastrated { get; private set; }
        public HelpStatusEnum HelpStatus { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime DateOfСreation { get; private set; }


        private Pet() { } // For EF Core
        private Pet(Guid id, string name, string generalDescription, string color, CharacteristicValueObject characteristic, HelpStatusEnum helpStatus) : base(id)
        {
            Nickname = name;
            GeneralDescription = generalDescription;
            Color = color;
            Characteristic = characteristic;
            HelpStatus = helpStatus;
            DateOfСreation = DateTime.UtcNow;
        }


        public static Guid NewId() => Guid.NewGuid();


        public static Result<Pet> Create(string name, string description, string breed, string color, double weight, double height, string phone, HelpStatusEnum helpStatus)
        {

            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("Name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Pet>("Description cannot be empty");

            if (string.IsNullOrWhiteSpace(breed))
                return Result.Failure<Pet>("Breed cannot be empty");

            if (string.IsNullOrWhiteSpace(color))
                return Result.Failure<Pet>("Color cannot be empty");

            var ch = CharacteristicValueObject.Create(weight, height);

            if (ch.IsFailure)
                return Result.Failure<Pet>(ch.Error);

            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<Pet>("Phone number  cannot be empty");

            if (helpStatus == default)
                return Result.Failure<Pet>("Pet help status is required");



            var pet = new Pet(NewId(), name, description, color, ch.Value, helpStatus);

            return Result.Success(pet);
        }


    }


    public enum HelpStatusEnum
    {
        None,
        NeedsHelp,
        LookingForAHome,
        FoundAHome
    }
}
