using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult GetApplication(Guid applicationId)
    {
        return  Ok("App works");
    }
}