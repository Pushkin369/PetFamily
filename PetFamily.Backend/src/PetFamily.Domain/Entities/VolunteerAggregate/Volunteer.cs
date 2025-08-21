using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;

namespace PetFamily.Domain.Entities.VolunteerAggregate
{
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<SocialNetwork> _socialNetworks = [];
        private readonly List<Pet> _pets = [];

        private Volunteer() : base(default!)
        {
        }

        public FullName FIO { private set; get; }
        public Phone Phone { get; private set; }
        public Email Email { get; private set; }
        public Requisites Requisites { get; private set; }
        public string? GeneralDescription { get; private set; }
        public int Experience { get; private set; }

        public IReadOnlyList<Pet> Pets => _pets;
        public SocialNetworkList SocialNetworksList { get; private set; }

        public int GetCountPetFoundAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.FoundAHome);
        public int GetCountPetLookingAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.LookingForAHome);
        public int GetCountPetNeedsHelp() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.NeedsHelp);

        // For EF Core
        private Volunteer(VolunteerId volunteerId) : base(volunteerId)
        {
        }

        private Volunteer(
            VolunteerId volunteerId,
            FullName fio,
            Phone phone,
            Email email,
            Requisites requisites,
            string generalDescription,
            int experience) : base(volunteerId)
        {
            FIO = fio;
            Phone = phone;
            Email = email;
            Requisites = requisites;
            GeneralDescription = generalDescription;
            Experience = experience;
            SocialNetworksList = new SocialNetworkList();
        }


        public static Result<Volunteer, Error> Create(
            FullName fio,
            Phone phone,
            Email email,
            Requisites requisites,
            string generalDescription,
            int experience)
        {
            if (string.IsNullOrWhiteSpace(generalDescription))
                return Errors.General.ValidationEmpty(generalDescription);

            if (experience < 0)
                return Errors.General.ValidationLength(experience.ToString(), "more than 0");

            return new Volunteer(VolunteerId.NewVolunteerId,
                fio, phone, email, requisites, generalDescription, experience);
        }

        public Result AddSocialNetwork(string name, string link)
        {
            var socialNetwork = SocialNetwork.Create(name, link);

            if (socialNetwork.IsFailure)
                return Result.Failure(socialNetwork.Error.Message);

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