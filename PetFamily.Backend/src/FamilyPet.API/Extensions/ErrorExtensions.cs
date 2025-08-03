using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;

namespace FamilyPet.API.Extensions;

public static class ErrorExtensions
{
    public static ActionResult ToActionResult(this Error error)
    {
        var problem = new { error.Code, error.Message};
        
        return error.Type switch
        {
            ErrorType.ValidationError => new BadRequestObjectResult(problem),
            ErrorType.NotFound => new NotFoundObjectResult(problem),
            ErrorType.Conflict => new ConflictObjectResult(problem),
            ErrorType.InternalError => new ObjectResult(problem) { StatusCode = 500 },
            _ => new ObjectResult(problem) { StatusCode = 500 }
        };
    }
}