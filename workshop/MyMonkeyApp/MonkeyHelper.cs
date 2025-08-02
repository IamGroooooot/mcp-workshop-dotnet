namespace MyMonkeyApp;

/// <summary>
/// Static helper class for managing monkey data and operations.
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> _monkeys = new()
    {
        new Monkey
        {
            Name = "Chimpanzee",
            Species = "Pan troglodytes",
            Location = "Central Africa",
            Population = 300000,
            Description = "Highly intelligent primates known for their tool use and complex social behaviors."
        },
        new Monkey
        {
            Name = "Orangutan",
            Species = "Pongo pygmaeus",
            Location = "Borneo & Sumatra",
            Population = 100000,
            Description = "Large, arboreal apes with distinctive reddish-brown hair and remarkable intelligence."
        },
        new Monkey
        {
            Name = "Bonobo",
            Species = "Pan paniscus",
            Location = "Democratic Republic of Congo",
            Population = 50000,
            Description = "Peaceful primates known for their matriarchal society and conflict resolution through affection."
        },
        new Monkey
        {
            Name = "Gorilla",
            Species = "Gorilla gorilla",
            Location = "Central and Eastern Africa",
            Population = 200000,
            Description = "The largest living primates, known for their gentle nature despite their imposing size."
        },
        new Monkey
        {
            Name = "Macaque",
            Species = "Macaca mulatta",
            Location = "Asia",
            Population = 2500000,
            Description = "Adaptable primates found in various habitats, from tropical forests to urban areas."
        },
        new Monkey
        {
            Name = "Baboon",
            Species = "Papio hamadryas",
            Location = "Africa and Arabian Peninsula",
            Population = 500000,
            Description = "Terrestrial primates living in large troops with complex social hierarchies."
        },
        new Monkey
        {
            Name = "Spider Monkey",
            Species = "Ateles geoffroyi",
            Location = "Central and South America",
            Population = 250000,
            Description = "Agile primates with long limbs and prehensile tails, excellent for swinging through trees."
        },
        new Monkey
        {
            Name = "Howler Monkey",
            Species = "Alouatta caraya",
            Location = "Central and South America",
            Population = 400000,
            Description = "Known for their loud vocalizations that can be heard up to 3 miles away."
        },
        new Monkey
        {
            Name = "Proboscis Monkey",
            Species = "Nasalis larvatus",
            Location = "Borneo",
            Population = 7000,
            Description = "Distinctive monkeys with large noses, excellent swimmers found only in Borneo's mangroves."
        },
        new Monkey
        {
            Name = "Golden Lion Tamarin",
            Species = "Leontopithecus rosalia",
            Location = "Brazil",
            Population = 3200,
            Description = "Small, endangered primates with golden manes, native to Brazil's Atlantic coastal forests."
        },
        new Monkey
        {
            Name = "Japanese Macaque",
            Species = "Macaca fuscata",
            Location = "Japan",
            Population = 114000,
            Description = "Also known as snow monkeys, famous for bathing in hot springs during winter."
        },
        new Monkey
        {
            Name = "Capuchin Monkey",
            Species = "Cebus capucinus",
            Location = "Central and South America",
            Population = 180000,
            Description = "Intelligent primates known for their tool use and problem-solving abilities."
        }
    };

    /// <summary>
    /// Gets all available monkeys.
    /// </summary>
    /// <returns>A read-only collection of all monkeys.</returns>
    public static IReadOnlyList<Monkey> GetAllMonkeys()
    {
        return _monkeys.AsReadOnly();
    }

    /// <summary>
    /// Gets a monkey by its name (case-insensitive search).
    /// </summary>
    /// <param name="name">The name of the monkey to search for.</param>
    /// <returns>The monkey if found, null otherwise.</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets a random monkey from the collection.
    /// </summary>
    /// <returns>A randomly selected monkey.</returns>
    public static Monkey GetRandomMonkey()
    {
        var random = new Random();
        var index = random.Next(_monkeys.Count);
        return _monkeys[index];
    }

    /// <summary>
    /// Gets the total count of monkeys in the collection.
    /// </summary>
    /// <returns>The number of monkeys available.</returns>
    public static int GetMonkeyCount()
    {
        return _monkeys.Count;
    }
}