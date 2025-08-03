using Microsoft.AspNetCore.Mvc;

namespace FamilyPet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
    }
}
