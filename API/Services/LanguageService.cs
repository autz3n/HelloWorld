using System.Diagnostics;
using Messages;

namespace API.Services;

public class LanguageService
{
    private static LanguageService? _instance;
    
    public static LanguageService Instance
    {
        get { return _instance ??= new LanguageService(); }
    }
    
    private LanguageService()
    { }
    
    public LanguageResponse GetLanguages()
    {
        using var activity = MonitorService.ActivitySource.StartActivity("LanguageService");
        MonitorService.Log.Information("In LanguageService, using GreetingService to retrieve languages.");

        try
        {
            var languages = GreetingService.Instance.GetLanguages();
            MonitorService.Log.Debug("Retrieved {Amount} of languages from GreetingService");
            return new LanguageResponse { Languages = languages };
        }
        catch (Exception ex)
        {
            MonitorService.Log.Error("Error while retrieving languages from GreetingService", ex);
            throw;
        }
    }
}