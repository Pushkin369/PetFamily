using CSharpFunctionalExtensions;


namespace PetFamily.Domain.VolunteerAggregate
{
    public class SocialNetworkValueObject
    {
        public string Name { get; }
        public string Link { get; }

        private SocialNetworkValueObject(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public static Result<SocialNetworkValueObject> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialNetworkValueObject>("Social network name is required");

            if (string.IsNullOrWhiteSpace(link))
                return Result.Failure<SocialNetworkValueObject>("Link is required");

            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return Result.Failure<SocialNetworkValueObject>("Link is not a valid URL");

            return Result.Success(new SocialNetworkValueObject(name, link));
        }
    }
}
