using PetFamily.Domain.Shared;

namespace PetFamily.Application.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    public object? Result { get; }
    public List<ResponseError> Errors { get; } 
    public DateTime TimeGenereted { get; }

    private Envelope(object? result, IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenereted = DateTime.Now;
    }
    
    public static Envelope Ok(object? result = null) =>
        new(result, []);

    public static Envelope Error(IEnumerable<ResponseError> errors) =>
        new(null, errors);
}