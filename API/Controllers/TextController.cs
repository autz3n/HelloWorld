using API.Models;
using API.Services;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(string languageCode)
    {
        using var activity = MonitorService.ActivitySource.StartActivity("TextControllerTrace");
        MonitorService.Log.Information("Text API Controller");
        try
        {
            var greeting = GreetingService.Instance.Greet(new GreetingRequest { LanguageCode = languageCode });
            var planet = PlanetService.Instance.GetPlanet();
        
            var response = new GetGreetingModel.Response
            {
                Greeting = greeting.Greeting,
                Planet = planet.Planet
            };
            MonitorService.Log.Debug("Text API Controller response: " + response.Greeting);
            return Ok(response);
        }
        catch (Exception e)
        {
            MonitorService.Log.Error(e, "Text API Controller hit catch block with error" + e.Message);
            return StatusCode(500, "An error occurred");
        }
    }
}