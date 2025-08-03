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
using PetFamily.Domain.Shared;

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

        private Pet() : base(default!) { }// For EF Core
        private Pet(PetId petId) : base(petId) { } 

        private Pet(
            PetId petId, 
            string name, 
            string color, 
            string generalDescription,
            string infoAboutHealthcharacteristic,
            Phone ownerPhoneNumber, 
            Address address, 
            Characteristic characteristic, 
            Requisites requisites,
            bool isVaccinated, 
            bool isCastrated, 
            HelpStatusEnum helpStatus, 
            DateOnly dateOfBirth) : base(petId)
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

        public static Result<Pet, Error> Create(
            string name, 
            string color, 
            string generalDescription, 
            string infoAboutHealth,
            string ownerPhoneNumber,
            string country, string state, string city, string street, string house, 
            double weight, double height,
            string nameReq, string descriptionReq, string descriptionTransferReq, 
            bool isVaccinated, bool isCastrated,
            HelpStatusEnum helpStatus, DateOnly dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (string.IsNullOrWhiteSpace(color))
                return Errors.General.ValidationEmpty(color);

            if (string.IsNullOrWhiteSpace(generalDescription))
                return Errors.General.ValidationEmpty(generalDescription);

            if (string.IsNullOrWhiteSpace(infoAboutHealth))
                return Errors.General.ValidationEmpty(infoAboutHealth);

            var phoneResult = Phone.Create(ownerPhoneNumber);
            if (phoneResult.IsFailure)
                return phoneResult.Error;

            var addressResult = Address.Create(country, state, city, street, house);
            if (addressResult.IsFailure)
                return addressResult.Error;

            var chResult = Characteristic.Create(weight, height);
            if (chResult.IsFailure)
                return chResult.Error;

            var reqResult = Requisites.Create(nameReq, descriptionReq, descriptionTransferReq);
            if (reqResult.IsFailure)
                return reqResult.Error;

            if (helpStatus == default)
                return Errors.General.ValidationEmpty(helpStatus.ToString());

            if (dateOfBirth == default)
                return Errors.General.ValidationEmpty(dateOfBirth.ToString());

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
                 return Errors.General.ValidationEmpty(dateOfBirth.ToString());

            return new Pet(
                PetId.NewPetId, name, color, generalDescription, infoAboutHealth,
                phoneResult.Value, addressResult.Value, chResult.Value, reqResult.Value,
                isVaccinated, isCastrated, helpStatus, dateOfBirth);
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