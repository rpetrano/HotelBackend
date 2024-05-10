using Microsoft.AspNetCore.Mvc;

[ApiController]
public class HealthcheckController : ControllerBase 
{
    [HttpGet("health")]
    public IActionResult Test()
    {
        // TODO: check DB connection
        return Ok("Ok"); 
    }
}