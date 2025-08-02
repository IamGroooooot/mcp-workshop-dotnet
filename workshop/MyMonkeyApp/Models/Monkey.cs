namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a monkey species or individual monkey with location and population data
/// </summary>
public class Monkey
{
    /// <summary>
    /// The name of the monkey species or individual monkey
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The geographic location where the monkey is found
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description about the monkey species or individual
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// URL to an image of the monkey
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Population count of the species or individual
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Latitude coordinate of the monkey's location
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude coordinate of the monkey's location
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Determines if this is a wild species (population > 1) or individual monkey
    /// </summary>
    public bool IsWildSpecies => Population > 1;

    /// <summary>
    /// Returns a formatted population string
    /// </summary>
    public string FormattedPopulation => Population == 1 ? "Individual" : Population.ToString("N0");

    /// <summary>
    /// Returns formatted coordinates as a string
    /// </summary>
    public string Coordinates => $"{Latitude:F6}, {Longitude:F6}";

    /// <summary>
    /// Default constructor
    /// </summary>
    public Monkey() { }

    /// <summary>
    /// Constructor with all required properties
    /// </summary>
    /// <param name="name">Monkey name</param>
    /// <param name="location">Geographic location</param>
    /// <param name="details">Detailed description</param>
    /// <param name="image">Image URL</param>
    /// <param name="population">Population count</param>
    /// <param name="latitude">Latitude coordinate</param>
    /// <param name="longitude">Longitude coordinate</param>
    public Monkey(string name, string location, string details, string image, int population, double latitude, double longitude)
    {
        Name = name;
        Location = location;
        Details = details;
        Image = image;
        Population = population;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Returns a string representation of the monkey
    /// </summary>
    public override string ToString()
    {
        return $"{Name} - {Location} (Population: {FormattedPopulation})";
    }
}
