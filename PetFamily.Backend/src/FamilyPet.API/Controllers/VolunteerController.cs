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
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {


            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToActionResult();

            return Ok(result.Value);
        }
    }
}