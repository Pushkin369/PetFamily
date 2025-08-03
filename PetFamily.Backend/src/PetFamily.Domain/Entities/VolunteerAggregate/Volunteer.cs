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

namespace PetFamily.Domain.Entities.VolunteerAggregate
{
    public record VolunteerId(Guid Value)
    {
        public static VolunteerId NewVolunteerId => new VolunteerId(Guid.NewGuid());

        public static VolunteerId Empty => new VolunteerId(Guid.Empty);

        public static VolunteerId Create(Guid id) => new VolunteerId(id);
    }

    public record SocialNetworkList()
    {
        public List<SocialNetwork> SocialNetworks { get; private set; }
    }

    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<SocialNetwork> _socialNetworks = [];
        private readonly List<Pet> _pets = [];
        private Volunteer() : base(default!) { }
        public FullName FIO { private set; get; }
        public Phone Phone { get; private set; }
        public Email Email { get; private set; }
        public Requisites Requisites { get; private set; }
        public string? GeneralDescription { get; private set; }
        public int Experience { get; private set; }

        public IReadOnlyList<Pet> Pets => _pets;
        public SocialNetworkList  SocialNetworksList { get; private set; }

        public int GetCountPetFoundAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.FoundAHome);
        public int GetCountPetLookingAHome() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.LookingForAHome);
        public int GetCountPetNeedsHelp() => Pets.Count(p => p.HelpStatus == HelpStatusEnum.NeedsHelp);


        private Volunteer(VolunteerId volunteerId) : base(volunteerId)
        {
        } // For EF Core

        private Volunteer(
            VolunteerId volunteerId, 
            FullName fio, 
            Phone phone, 
            Email email, 
            Requisites requisites,
            string generalDescription, int experience) : base(volunteerId)
        {
            FIO = fio;
            Phone = phone;
            Email = email;
            Requisites = requisites;
            GeneralDescription = generalDescription;
            Experience = experience;
        }


        public static Result<Volunteer, Error> Create(
            string firstname, string surname, string patronymic,
            string phone, 
            string email, 
            string nameReq, string descriptionReq, string descriptionTransferReq,
            string description, int experience)
        {
            var fioResult = FullName.Create(firstname, surname, patronymic);
            if (fioResult.IsFailure)
                return fioResult.Error;

            var phoneResult = Phone.Create(phone);
            if (phoneResult.IsFailure)
                return phoneResult.Error;
            
            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var reqResult = Requisites.Create(nameReq, descriptionReq, descriptionTransferReq);
            if (reqResult.IsFailure)
                return reqResult.Error;

            if (string.IsNullOrWhiteSpace(description))
                return Errors.General.ValidationEmpty(description);

            if (experience < 0)
                return Errors.General.ValidationLength(experience.ToString(), "more than 0");

            return new Volunteer(
                VolunteerId.NewVolunteerId, fioResult.Value,
                phoneResult.Value, emailResult.Value,
                reqResult.Value, description, experience);
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