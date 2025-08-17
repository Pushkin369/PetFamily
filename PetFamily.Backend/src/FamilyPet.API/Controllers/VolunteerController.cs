using FamilyPet.API.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Response;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;

namespace FamilyPet.API.Controllers
{
    public class VolunteerController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromServices] IValidator<CreateVolunteerRequest> validator,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var  validationErrors = validationResult.Errors;
                List<ResponseError> errors = [];
                foreach (var validationError in validationErrors)
                {
                    var error = Error.ValidationError(validationError.ErrorCode, validationError.ErrorMessage);
                    var responceError = new ResponseError(error.Code, error.Message, validationError.PropertyName);
                    errors.Add(responceError);
                }

                var envelope = Envelope.Error(errors);
                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToActionResult();

            return Ok(result.Value);
        }
    }
}