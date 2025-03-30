using Microsoft.AspNetCore.WebUtilities;

namespace API.Services;

public class GreetingService
{
    private static GreetingService? _instance;
    
    public static GreetingService Instance
    {
        get { return _instance ??= new GreetingService(); }
    }
    
    private GreetingService()
    { }
    
    public GreetingResponse Greet(Messages.GreetingRequest request)
    {
        
        using var activity = MonitorService.ActivitySource.StartActivity("GreetingService");
        MonitorService.Log.Debug("Calling GreetingService.Greet() for language: {Language}", request.LanguageCode);
        
        var language = request.LanguageCode;
        var greeting = language switch
        {
            "en" => "Hello",
            "es" => "Hola",
            "fr" => "Bonjour",
            "de" => "Hallo",
            "it" => "Ciao",
            "pt" => "Olá",
            "ru" => "Привет",
            "zh" => "你好",
            "ja" => "こんにちは",
            "ar" => "مرحبا",
            "hi" => "नमस्ते",
            "sw" => "Hujambo"
        };
        MonitorService.Log.Debug("Created Greeting: {Greeting} in language: {Language}", greeting, language);
        return new GreetingResponse { Greeting = greeting };
    }
    
    public string[] GetLanguages()
    {
        MonitorService.Log.Information("Using string array from GreetingService");
        return new [] { "en", "es", "fr", "de", "it", "pt", "ru", "zh", "ya", "ar", "hi", "sw" };
    }
}