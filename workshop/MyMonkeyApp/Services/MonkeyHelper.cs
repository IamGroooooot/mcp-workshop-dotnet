using MyMonkeyApp.Models;

namespace MyMonkeyApp.Services;

/// <summary>
/// Static helper class for managing monkey data and integrating with MCP server
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey> _monkeys = new();
    private static readonly Random _random = new();
    private static int _randomAccessCount = 0;
    private static bool _isDataLoaded = false;

    /// <summary>
    /// Gets the total number of times a random monkey has been accessed
    /// </summary>
    public static int RandomAccessCount => _randomAccessCount;

    /// <summary>
    /// Gets the total number of monkeys in the collection
    /// </summary>
    public static int TotalMonkeys => _monkeys.Count;

    /// <summary>
    /// Indicates whether monkey data has been loaded from the MCP server
    /// </summary>
    public static bool IsDataLoaded => _isDataLoaded;

    /// <summary>
    /// Loads monkey data from hardcoded collection (simulating MCP server response)
    /// In a real implementation, this would call the actual MCP server
    /// </summary>
    public static void LoadMonkeyData()
    {
        if (_isDataLoaded)
            return;

        // Hardcoded monkey data from MCP server response
        _monkeys = new List<Monkey>
        {
            new("Baboon", "Africa & Asia", "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg", 10000, -8.783195, 34.508523),
            new("Capuchin Monkey", "Central & South America", "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg", 23000, 12.769013, -85.602364),
            new("Blue Monkey", "Central and East Africa", "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/bluemonkey.jpg", 12000, 1.957709, 37.297204),
            new("Squirrel Monkey", "Central & South America", "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/saimiri.jpg", 11000, -8.783195, -55.491477),
            new("Golden Lion Tamarin", "Brazil", "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg", 19000, -14.235004, -51.92528),
            new("Howler Monkey", "South America", "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/alouatta.jpg", 8000, -8.783195, -55.491477),
            new("Japanese Macaque", "Japan", "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg", 1000, 36.204824, 138.252924),
            new("Mandrill", "Southern Cameroon, Gabon, and Congo", "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/mandrill.jpg", 17000, 7.369722, 12.354722),
            new("Proboscis Monkey", "Borneo", "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/borneo.jpg", 15000, 0.961883, 114.55485),
            new("Sebastian", "Seattle", "This little trouble maker lives in Seattle with James and loves traveling on adventures with James and tweeting @MotzMonkeys. He by far is an Android fanboy and is getting ready for the new Google Pixel 9!", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg", 1, 47.606209, -122.332071),
            new("Henry", "Phoenix", "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. His favorite platform is iOS by far and is excited for the new iPhone Xs!", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/henry.jpg", 1, 33.448377, -112.074037),
            new("Red-shanked douc", "Vietnam", "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. The douc is an arboreal and diurnal monkey that eats and sleeps in the trees of the forest.", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/douc.jpg", 1300, 16.111648, 108.262122),
            new("Mooch", "Seattle", "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. Her favorite platform is iOS by far and is excited for the new iPhone 16!", "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/Mooch.PNG", 1, 47.608013, -122.335167)
        };

        _isDataLoaded = true;
    }

    /// <summary>
    /// Gets all monkeys in the collection
    /// </summary>
    /// <returns>A read-only list of all monkeys</returns>
    public static IReadOnlyList<Monkey> GetAllMonkeys()
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        return _monkeys.AsReadOnly();
    }

    /// <summary>
    /// Gets a random monkey from the collection and increments the access counter
    /// </summary>
    /// <returns>A randomly selected monkey, or null if no monkeys are available</returns>
    public static Monkey? GetRandomMonkey()
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        if (_monkeys.Count == 0)
            return null;

        var randomIndex = _random.Next(_monkeys.Count);
        _randomAccessCount++;
        
        return _monkeys[randomIndex];
    }

    /// <summary>
    /// Finds a monkey by name (case-insensitive search)
    /// </summary>
    /// <param name="name">The name of the monkey to find</param>
    /// <returns>The monkey with the specified name, or null if not found</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all wild monkey species (population > 1)
    /// </summary>
    /// <returns>A list of wild monkey species</returns>
    public static IReadOnlyList<Monkey> GetWildSpecies()
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        return _monkeys.Where(m => m.IsWildSpecies).ToList().AsReadOnly();
    }

    /// <summary>
    /// Gets all individual monkeys (population = 1)
    /// </summary>
    /// <returns>A list of individual monkeys</returns>
    public static IReadOnlyList<Monkey> GetIndividualMonkeys()
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        return _monkeys.Where(m => !m.IsWildSpecies).ToList().AsReadOnly();
    }

    /// <summary>
    /// Finds monkeys by location (case-insensitive partial match)
    /// </summary>
    /// <param name="location">The location to search for</param>
    /// <returns>A list of monkeys in the specified location</returns>
    public static IReadOnlyList<Monkey> GetMonkeysByLocation(string location)
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        if (string.IsNullOrWhiteSpace(location))
            return new List<Monkey>().AsReadOnly();

        return _monkeys.Where(m => 
            m.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
            .ToList().AsReadOnly();
    }

    /// <summary>
    /// Gets monkeys with population within a specified range
    /// </summary>
    /// <param name="minPopulation">Minimum population (inclusive)</param>
    /// <param name="maxPopulation">Maximum population (inclusive)</param>
    /// <returns>A list of monkeys within the population range</returns>
    public static IReadOnlyList<Monkey> GetMonkeysByPopulationRange(int minPopulation, int maxPopulation)
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        return _monkeys.Where(m => m.Population >= minPopulation && m.Population <= maxPopulation)
            .ToList().AsReadOnly();
    }

    /// <summary>
    /// Resets the random access counter
    /// </summary>
    public static void ResetRandomAccessCount()
    {
        _randomAccessCount = 0;
    }

    /// <summary>
    /// Gets statistics about the monkey collection
    /// </summary>
    /// <returns>A summary of monkey statistics</returns>
    public static string GetStatistics()
    {
        if (!_isDataLoaded)
            LoadMonkeyData();

        var wildSpecies = _monkeys.Count(m => m.IsWildSpecies);
        var individuals = _monkeys.Count(m => !m.IsWildSpecies);
        var totalPopulation = _monkeys.Sum(m => m.Population);
        var avgPopulation = _monkeys.Average(m => m.Population);

        return $"""
            Monkey Collection Statistics:
            - Total Monkeys: {TotalMonkeys}
            - Wild Species: {wildSpecies}
            - Individual Monkeys: {individuals}
            - Total Population: {totalPopulation:N0}
            - Average Population: {avgPopulation:N0}
            - Random Access Count: {RandomAccessCount}
            """;
    }
}
