using CSharpFunctionalExtensions;


namespace PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects
{
    public record SocialNetwork(string Name, string Link)
    {
        public static Result<SocialNetwork> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialNetwork>("Social network name is required");

            if (string.IsNullOrWhiteSpace(link))
                return Result.Failure<SocialNetwork>("Link is required");

            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return Result.Failure<SocialNetwork>("Link is not a valid URL");

            return Result.Success(new SocialNetwork(name, link));
        }
    }
}
