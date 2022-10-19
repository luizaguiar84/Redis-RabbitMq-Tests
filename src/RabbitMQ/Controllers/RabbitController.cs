using Microsoft.AspNetCore.Mvc;

namespace RabbitMQ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RabbitController : ControllerBase
{
    public RabbitController()
    {
        
    }
    
    [HttpGet("Consume")]
    public IActionResult Get()
    {
        return Ok();
    } 
    
    [HttpPost("Publish")]
    public IActionResult Post()
    {
        return Ok();
    } 
}