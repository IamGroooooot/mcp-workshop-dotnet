using MyMonkeyApp.Models;
using MyMonkeyApp.Services;

namespace MyMonkeyApp;

/// <summary>
/// Main program class for the Monkey App console application
/// </summary>
public class Program
{
    private static readonly string[] MonkeyAsciiArt = {
        @"
    🐒 Welcome to Monkey App! 🐒
        .="".""=._.-.
       /  ;  / .'  '.  \
      |   |  |       |  |
       \  |   \     /   |
        '._\ '. '-' .' /_.'
          jgs '-..-'
",
        @"
        🍌 Banana Time! 🍌
          .-'~~~-.
        .'o  oOOOo`.
       :~~~-.oOo   o`.
        `. \ ~-.  oOOo.
          `.; / ~.  OO:
          .'  ;-- `.o.'
         ,'  ; ~~--'~
         ;  ;
    _.-'~' ;
jgs( -'~~'-.)',
",
        @"
      🌴 Monkey Business! 🌴
           .--.
          /    \
         | o  o |
          \  >  /
          /|  ||\
         / |__|| \
        /  |--|  \
       /   |  |   \
      /    |  |    \
     /     |  |     \
    /______|  |______\
",
        @"
        🎪 Circus Monkey! 🎪
            /|   /|
           (  @ @  )
            )  .  (
           ( v__v )
          ^^^^^^^^^^
         /          \
        |  Go Bananas! |
         \____________/
"
    };

    /// <summary>
    /// Main entry point of the application
    /// </summary>
    public static void Main()
    {
        Console.Clear();
        ShowRandomAsciiArt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🐒 Welcome to the Amazing Monkey App! 🐒");
        Console.ResetColor();
        Console.WriteLine("Loading monkey data from MCP server...\n");
        
        // Initialize monkey data
        MonkeyHelper.LoadMonkeyData();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ Successfully loaded {MonkeyHelper.TotalMonkeys} monkeys!\n");
        Console.ResetColor();

        bool exitApp = false;
        
        while (!exitApp)
        {
            exitApp = ShowMainMenu();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n🍌 Thanks for using Monkey App! See you later! 🍌");
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the main menu and handles user input
    /// </summary>
    /// <returns>True if user wants to exit, false otherwise</returns>
    private static bool ShowMainMenu()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🐵 MAIN MENU 🐵");
        Console.ResetColor();
        Console.WriteLine(new string('=', 50));
        
        Console.WriteLine("1. 📋 List all monkeys");
        Console.WriteLine("2. 🔍 Get details for a specific monkey by name");
        Console.WriteLine("3. 🎲 Get a random monkey");
        Console.WriteLine("4. 📊 Show statistics");
        Console.WriteLine("5. 🚪 Exit app");
        
        Console.Write("\nEnter your choice (1-5): ");
        
        var input = Console.ReadLine();
        
        switch (input?.Trim())
        {
            case "1":
                ListAllMonkeys();
                break;
            case "2":
                GetMonkeyByName();
                break;
            case "3":
                GetRandomMonkey();
                break;
            case "4":
                ShowStatistics();
                break;
            case "5":
                return true;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid choice! Please enter 1, 2, 3, 4, or 5.");
                Console.ResetColor();
                break;
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        
        return false;
    }

    /// <summary>
    /// Lists all monkeys in a formatted table
    /// </summary>
    private static void ListAllMonkeys()
    {
        Console.Clear();
        ShowRandomAsciiArt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("📋 ALL MONKEYS");
        Console.ResetColor();
        Console.WriteLine(new string('=', 80));

        var monkeys = MonkeyHelper.GetAllMonkeys();
        
        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{"Name",-20} {"Location",-25} {"Population",-12} {"Type",-10}");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));

        foreach (var monkey in monkeys)
        {
            var color = monkey.IsWildSpecies ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.ForegroundColor = color;
            
            var type = monkey.IsWildSpecies ? "Wild" : "Individual";
            Console.WriteLine($"{monkey.Name,-20} {monkey.Location,-25} {monkey.FormattedPopulation,-12} {type,-10}");
        }
        
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Total: {monkeys.Count} monkeys");
        Console.ResetColor();
    }

    /// <summary>
    /// Gets details for a specific monkey by name
    /// </summary>
    private static void GetMonkeyByName()
    {
        Console.Clear();
        ShowRandomAsciiArt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🔍 FIND MONKEY BY NAME");
        Console.ResetColor();
        Console.WriteLine(new string('=', 50));

        Console.Write("Enter monkey name: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Please enter a valid monkey name.");
            Console.ResetColor();
            return;
        }

        var monkey = MonkeyHelper.GetMonkeyByName(name);

        if (monkey == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ No monkey found with name '{name}'.");
            Console.ResetColor();
            
            // Show suggestions
            var allMonkeys = MonkeyHelper.GetAllMonkeys();
            var suggestions = allMonkeys
                .Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Take(3)
                .ToList();

            if (suggestions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n💡 Did you mean:");
                Console.ResetColor();
                foreach (var suggestion in suggestions)
                {
                    Console.WriteLine($"   • {suggestion.Name}");
                }
            }
            return;
        }

        DisplayMonkeyDetails(monkey);
    }

    /// <summary>
    /// Gets and displays a random monkey
    /// </summary>
    private static void GetRandomMonkey()
    {
        Console.Clear();
        ShowRandomAsciiArt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🎲 RANDOM MONKEY");
        Console.ResetColor();
        Console.WriteLine(new string('=', 50));

        var monkey = MonkeyHelper.GetRandomMonkey();

        if (monkey == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ No monkeys available.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"🎯 Random monkey selected! (Access count: {MonkeyHelper.RandomAccessCount})");
        Console.ResetColor();
        Console.WriteLine();

        DisplayMonkeyDetails(monkey);
    }

    /// <summary>
    /// Displays detailed information about a monkey
    /// </summary>
    /// <param name="monkey">The monkey to display</param>
    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"🐒 {monkey.Name}");
        Console.ResetColor();
        Console.WriteLine(new string('-', 50));
        
        Console.WriteLine($"📍 Location: {monkey.Location}");
        Console.WriteLine($"👥 Population: {monkey.FormattedPopulation}");
        Console.WriteLine($"🌍 Coordinates: {monkey.Coordinates}");
        Console.WriteLine($"🏷️  Type: {(monkey.IsWildSpecies ? "Wild Species" : "Individual Monkey")}");
        Console.WriteLine($"🖼️  Image: {monkey.Image}");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("📝 Details:");
        Console.ResetColor();
        Console.WriteLine($"   {monkey.Details}");
    }

    /// <summary>
    /// Shows statistics about the monkey collection
    /// </summary>
    private static void ShowStatistics()
    {
        Console.Clear();
        ShowRandomAsciiArt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("📊 MONKEY STATISTICS");
        Console.ResetColor();
        Console.WriteLine(new string('=', 50));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(MonkeyHelper.GetStatistics());
        Console.ResetColor();
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🌍 Geographic Distribution:");
        Console.ResetColor();
        
        var monkeys = MonkeyHelper.GetAllMonkeys();
        var locationGroups = monkeys
            .GroupBy(m => m.Location)
            .OrderByDescending(g => g.Count())
            .Take(5);

        foreach (var group in locationGroups)
        {
            Console.WriteLine($"   • {group.Key}: {group.Count()} monkey(s)");
        }
    }

    /// <summary>
    /// Displays random ASCII art
    /// </summary>
    private static void ShowRandomAsciiArt()
    {
        var random = new Random();
        var artIndex = random.Next(MonkeyAsciiArt.Length);
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(MonkeyAsciiArt[artIndex]);
        Console.ResetColor();
    }
}
