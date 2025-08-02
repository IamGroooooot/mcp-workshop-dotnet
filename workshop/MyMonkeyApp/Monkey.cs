namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with its characteristics and habitat information.
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the common name of the monkey.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scientific species name of the monkey.
    /// </summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the primary location or habitat of the monkey.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated population count of the monkey species.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets a description of the monkey species.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Returns a formatted string representation of the monkey.
    /// </summary>
    /// <returns>A string containing the monkey's information.</returns>
    public override string ToString()
    {
        return $"{Name} ({Species})\n   Location: {Location}\n   Population: {Population:N0}\n   Description: {Description}";
    }
}