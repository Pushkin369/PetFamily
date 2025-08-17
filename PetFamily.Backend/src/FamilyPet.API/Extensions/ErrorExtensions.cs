using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Response;
using PetFamily.Domain.Shared;

namespace FamilyPet.API.Extensions;

public static class ErrorExtensions
{
    public static ActionResult ToActionResult(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.ValidationError => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.InternalError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var  responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([ responseError]);

        return new ObjectResult(envelope) { StatusCode = statusCode };
    }
}