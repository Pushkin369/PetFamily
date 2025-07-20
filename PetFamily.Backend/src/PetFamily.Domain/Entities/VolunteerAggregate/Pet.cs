using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PetFamily.Domain.Entities.SpeciesAggregate;

namespace PetFamily.Domain.Entities.VolunteerAggregate
{
    public record PetId(Guid Value)
    {
        public static PetId NewPetId => new PetId(Guid.NewGuid());

        public static PetId Empty => new PetId(Guid.Empty);

        public static PetId Create(Guid id) => new PetId(id);
    }


    public class Pet : Shared.Entity<PetId>
    {
        public SpeciesId SpeciesId { get; private set; }
        public BreedId BreedId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public string? GeneralDescription { get; private set; }
        public string? InfoAboutHealth { get; private set; }

        public Phone OwnerPhoneNumber { get; private set; }

        public Address Address { get; private set; }
        public Characteristic Characteristic { get; private set; }
        public Requisites Requisites { get; private set; }
        public bool IsVaccinated { get; private set; }
        public bool IsCastrated { get; private set; }
        public HelpStatusEnum HelpStatus { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime DateOfСreation { get; private set; }

        private Pet() : base(default!) { }

        private Pet(PetId petId) : base(petId)
        {
        } // For EF Core

        private Pet(PetId petId, string name, string color, string generalDescription,
            string infoAboutHealthcharacteristic,
            Phone ownerPhoneNumber, Address address, Characteristic characteristic, Requisites requisites,
            bool isVaccinated, bool isCastrated, HelpStatusEnum helpStatus, DateOnly dateOfBirth) : base(petId)
        {
            Name = name;
            Color = color;
            GeneralDescription = generalDescription;
            InfoAboutHealth = infoAboutHealthcharacteristic;
            OwnerPhoneNumber = ownerPhoneNumber;
            Address = address;
            Characteristic = characteristic;
            Requisites = requisites;
            IsVaccinated = isVaccinated;
            IsCastrated = isCastrated;
            HelpStatus = helpStatus;
            DateOfBirth = dateOfBirth;
            DateOfСreation = DateTime.UtcNow;
        }

        public static Result<Pet> Create(string name, string color, string generalDescription, string infoAboutHealth,
            string ownerPhoneNumber,
            string country, string state, string city, string street, string house, double weight, double height,
            string nameReq, string descriptionReq, string descriptionTransferReq, bool isVaccinated, bool isCastrated,
            HelpStatusEnum helpStatus, DateOnly dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("Name cannot be empty");

            if (string.IsNullOrWhiteSpace(color))
                return Result.Failure<Pet>("Color cannot be empty");

            if (string.IsNullOrWhiteSpace(generalDescription))
                return Result.Failure<Pet>("Description cannot be empty");

            if (string.IsNullOrWhiteSpace(infoAboutHealth))
                return Result.Failure<Pet>("Description cannot be empty");

            var ph = Phone.Create(ownerPhoneNumber);
            if (ph.IsFailure)
                return Result.Failure<Pet>(ph.Error);

            var address = Address.Create(country, state, city, street, house);
            if (address.IsFailure)
                return Result.Failure<Pet>(address.Error);

            var ch = Characteristic.Create(weight, height);
            if (ch.IsFailure)
                return Result.Failure<Pet>(ch.Error);

            var req = Requisites.Create(nameReq, descriptionReq, descriptionTransferReq);
            if (req.IsFailure)
                return Result.Failure<Pet>(req.Error);

            if (helpStatus == default)
                return Result.Failure<Pet>("Pet help status is required");

            if (dateOfBirth == default)
                return Result.Failure<Pet>("Date of birth is required");

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
                return Result.Failure<Pet>("Date of birth cannot be in the future");

            var pet = new Pet(PetId.NewPetId, name, color, generalDescription, infoAboutHealth,
                ph.Value, address.Value, ch.Value, req.Value, isVaccinated, isCastrated, helpStatus, dateOfBirth);

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