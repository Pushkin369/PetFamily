namespace PetFamily.Domain.Shared;

public record Error(string Code, string Message, ErrorType Type)
{
    public static Error ValidationError(string code, string message)
    {
        return new Error(code, message, ErrorType.ValidationError);
    }

    public static Error NotFound(string code, string message)
    {
        return new Error(code, message, ErrorType.NotFound);
    }

    public static Error InternalError(string code, string message)
    {
        return new Error(code, message, ErrorType.InternalError);
    }

    public static Error AlreadyExists(string code, string message)
    {
         return new Error(code, message, ErrorType.Conflict);
    }
}