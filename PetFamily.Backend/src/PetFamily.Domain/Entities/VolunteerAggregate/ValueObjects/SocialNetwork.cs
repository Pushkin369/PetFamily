using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;


namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record SocialNetwork(string Name, string Link)
    {
        public static Result<SocialNetwork,Error> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValidationEmpty(name);

            if (string.IsNullOrWhiteSpace(link))
                return Errors.General.ValidationEmpty(name);

            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return Errors.General.ValidationFormat(link,"https://localhost");

            return new SocialNetwork(name, link);
        }
    }
}
