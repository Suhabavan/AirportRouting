using AirportRoutingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[ApiController]
[Route("api/airports")]
public class AirportController : ControllerBase
{
    private readonly IAirportService _airportService;

    public AirportController(IAirportService airportService)
    {
        _airportService = airportService;
    }

    [HttpGet]
    public IActionResult GetDestinations(string originAirportCode)
    {
        try
        {
            Log.Information($"Request started for origin airport: {originAirportCode}");
            List<string> destinations = _airportService.GetDestinations(originAirportCode);

            if (destinations.Count == 0)
            {
                Log.Warning($"No destinations found for origin airport: {originAirportCode}");
                return NotFound($"No destinations found for origin airport: {originAirportCode}");
            }

            Log.Information($"Request completed for origin airport: {originAirportCode}");
            return Ok(destinations);
        }
        catch (ArgumentException ex)
        {
            Log.Error($"Error in AirportController: {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error($"Error in AirportController: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}