using FlightController.Common.Models;
using FlightController.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightController.Web.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly ILogger<FlightController> _logger;
    private readonly IFlightManager _flightManager;

    public FlightController(ILogger<FlightController> logger, IFlightManager flightManager)
    {
        _logger = logger;
        _flightManager = flightManager;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Flight>> CreateAsync([FromBody] Flight flight)
    {
        await _flightManager.CreateAsync(flight);
        return Ok(flight);
    }

    [HttpGet("GetFlights")]
    public async Task<ActionResult<List<Flight>>> GetFlights()
    {
        return await _flightManager.GetLastInQ();
    }
}