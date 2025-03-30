namespace API.Services;

public class PlanetService
{
    private static PlanetService? _instance;
    
    public static PlanetService Instance
    {
        get { return _instance ??= new PlanetService(); }
    }
    
    public PlanetResponse GetPlanet()
    {
        
        using var activity = MonitorService.ActivitySource.StartActivity("PlanetService");
        MonitorService.Log.Information("Calling PlanetService.GetPlanet() to retrieve planet");
        
        var planets = new[]
        {
            "Mercury",
            "Venus",
            "Earth",
            "Mars",
            "Jupiter",
            "Saturn",
            "Uranus",
            "Neptune"
        };

        var index = new Random(DateTime.Now.Millisecond).Next(1, planets.Length+1);
        var selectedPlanet = planets[index];
        MonitorService.Log.Debug("Selected Planet: {Planet}", selectedPlanet);
        return new PlanetResponse
        {
            Planet = planets[index]
        };
    }
}