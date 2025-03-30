using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LanguageController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        using var activity = MonitorService.ActivitySource.StartActivity("LanguageControllerTrace");
        MonitorService.Log.Information("Language API Controller, Getting Endpoint and Method");

        try
        {
            var language = LanguageService.Instance.GetLanguages();
            MonitorService.Log.Debug(
                "Was Successfull in retrieving languages. Default {DefaultLanguage}, Amount: {LanguageAmount}",
                language.DefaultLanguage, language.Languages?.Length ?? 0);
            return Ok(new GetLanguageModel.Response
                { DefaultLanguage = language.DefaultLanguage, Languages = language.Languages });
        }
        catch (Exception ex)
        {
            MonitorService.Log.Error("LanguageController: Failed to get languages");
            return StatusCode(500, "Error fetching languages");
        }
    }
}