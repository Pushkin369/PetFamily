namespace PetFamily.Domain.Shared;

public record Error(string Code, string Message, ErrorType Type)
{
    public const string SEPARATOR = "||";
    public static Error ValidationError(string code, string message) =>
        new Error(code, message, ErrorType.ValidationError);
    public static Error NotFound(string code, string message) =>
        new Error(code, message, ErrorType.NotFound);
    public static Error InternalError(string code, string message) =>
        new Error(code, message, ErrorType.InternalError);
    public static Error AlreadyExists(string code, string message) =>
        new Error(code, message, ErrorType.Conflict);

    public string Serialize()=> string.Join(SEPARATOR, Code, Message, Type);

    public static Error Deserialize(string serialize)
    {
    var parts = serialize.Split(SEPARATOR);
    
    if(parts.Length < 3)
        throw new ArgumentException("Invalid serialization format");
    
    if(Enum.TryParse<ErrorType>(parts[2], out ErrorType errorType)==false)
        throw new ArgumentException("Invalid serialization format");
    
    return new Error(parts[0], parts[1], errorType);
    }

}