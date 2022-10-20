using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Entities;
using RabbitMQ.Services;

namespace RabbitMQ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RabbitController : ControllerBase
{
    private readonly  IRabbitMqService _service;
    
    public RabbitController(IRabbitMqService service)
    {
        _service = service;
    }
    
    [HttpGet("Consume")]
    public IActionResult Get()
    {
        var response = _service.Consume();
        return Ok(response);
    } 
    
    [HttpPost("Publish")]
    public IActionResult Post([FromBody] Post post)
    {
        _service.Publish(post);
        return Ok();
    } 
}