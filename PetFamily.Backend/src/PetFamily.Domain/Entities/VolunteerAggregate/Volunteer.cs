using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetFamily.Domain.Entities.VolunteerAggregate
{
    public class Volunteer : Entity<Guid>
    {
        private readonly List<SocialNetwork> _socialNetworks = [];
        private readonly List<Pet> _pets = [];
        public FullName FIO { get; private set; }
        public string Email { get; private set; }
        public string? GeneralDescription { get; private set; }
        public int Experience { get; private set; }
        public string PhoneNumber { get; private set; }
        public IReadOnlyList<Pet> Pets => _pets;
        public IReadOnlyList<SocialNetwork> SocialNetworksValueObject => _socialNetworks;
        public Requisites Requisites { get; private set; }
        public int GetCountPetFoundAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.FoundAHome);
        public int GetCountPetLookingAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.LookingForAHome);
        public int GetCountPetNeedsHelp() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.NeedsHelp);


        private Volunteer() { } // For EF Core
        public Volunteer(Guid id, FullName fio, string email, string description, int experience, string phoneNumber) : base(id)
        {
            FIO = fio;
            Email = email;
            GeneralDescription = description;
            Experience = experience;
            PhoneNumber = phoneNumber;
        }


        public static Guid NewId() => Guid.NewGuid();
        public static Result<Volunteer> Create(string firstname, string surname, string patronymic, string email, string description, int experience, string phone)
        {
            var fioResult = FullName.Create(firstname, surname, patronymic);

            if (fioResult.IsFailure)
                return Result.Failure<Volunteer>(fioResult.Error);


            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Volunteer>("Email cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Volunteer>("Description cannot be empty");


            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<Volunteer>("Phone Number cannot be empty");

            var volunteer = new Volunteer(NewId(), fioResult.Value, email, description, experience, phone);

            return Result.Success(volunteer);
        }
        public Result AddSocialNetwork(string name, string link)
        {
            var socialNetwork = SocialNetwork.Create(name, link);

            if (socialNetwork.IsFailure)
                return Result.Failure(socialNetwork.Error);

            _socialNetworks.Add(socialNetwork.Value);

            return Result.Success();
        }
        public Result AddPet(Pet? pet)
        {
            if (pet is null)
                return Result.Failure("Pet cannot be null to add");

            _pets.Add(pet);

            return Result.Success();
        }
    }
}
