using FamilyPet.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;

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