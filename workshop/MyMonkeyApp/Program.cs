using MyMonkeyApp;

/// <summary>
/// Main program class for the Monkey Console Application.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point of the application.
    /// </summary>
    static void Main()
    {
        ShowWelcomeMessage();
        
        bool keepRunning = true;
        while (keepRunning)
        {
            ShowMainMenu();
            string? input = Console.ReadLine();
            
            keepRunning = ProcessMenuChoice(input);
            
            if (keepRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        ShowGoodbyeMessage();
    }

    /// <summary>
    /// Displays the welcome message with ASCII art.
    /// </summary>
    static void ShowWelcomeMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒");
        Console.WriteLine("🐒     Welcome to the Monkey Console App!     🐒");
        Console.WriteLine("🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒");
        Console.ResetColor();
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("        🐵");
        Console.WriteLine("       /   \\");
        Console.WriteLine("      |  o o |");
        Console.WriteLine("      |  ^   |");
        Console.WriteLine("      | (_)  |");
        Console.WriteLine("       \\___/");
        Console.WriteLine("        | |");
        Console.WriteLine("       /   \\");
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Displays the main menu options.
    /// </summary>
    static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=====================================");
        Console.WriteLine("           MAIN MENU");
        Console.WriteLine("=====================================");
        Console.ResetColor();
        
        Console.WriteLine("1. 📋 List all monkeys");
        Console.WriteLine("2. 🔍 Get monkey by name");
        Console.WriteLine("3. 🎲 Get random monkey");
        Console.WriteLine("4. 🚪 Exit");
        Console.WriteLine();
        Console.Write("Please select an option (1-4): ");
    }

    /// <summary>
    /// Processes the user's menu choice.
    /// </summary>
    /// <param name="input">The user's input.</param>
    /// <returns>True to continue running, false to exit.</returns>
    static bool ProcessMenuChoice(string? input)
    {
        switch (input?.Trim())
        {
            case "1":
                ListAllMonkeys();
                return true;
            case "2":
                GetMonkeyByName();
                return true;
            case "3":
                GetRandomMonkey();
                return true;
            case "4":
                return false;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Invalid option! Please enter a number between 1 and 4.");
                Console.ResetColor();
                return true;
        }
    }

    /// <summary>
    /// Lists all available monkeys.
    /// </summary>
    static void ListAllMonkeys()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("📋 All Available Monkeys:");
        Console.WriteLine("========================");
        Console.ResetColor();
        
        var monkeys = MonkeyHelper.GetAllMonkeys();
        for (int i = 0; i < monkeys.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{i + 1}. {monkeys[i].Name} ({monkeys[i].Species})");
            Console.ResetColor();
            Console.WriteLine($"   📍 Location: {monkeys[i].Location}");
            Console.WriteLine($"   👥 Population: {monkeys[i].Population:N0}");
            Console.WriteLine($"   📝 {monkeys[i].Description}");
        }
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n💡 Total species available: {MonkeyHelper.GetMonkeyCount()}");
        Console.ResetColor();
    }

    /// <summary>
    /// Gets a monkey by name based on user input.
    /// </summary>
    static void GetMonkeyByName()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🔍 Search for a Monkey:");
        Console.WriteLine("======================");
        Console.ResetColor();
        
        Console.Write("Enter the monkey name: ");
        string? name = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Please enter a valid monkey name.");
            Console.ResetColor();
            return;
        }
        
        var monkey = MonkeyHelper.GetMonkeyByName(name);
        if (monkey != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Monkey found!");
            Console.WriteLine("================");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🐒 {monkey.Name} ({monkey.Species})");
            Console.ResetColor();
            Console.WriteLine($"📍 Location: {monkey.Location}");
            Console.WriteLine($"👥 Population: {monkey.Population:N0}");
            Console.WriteLine($"📝 {monkey.Description}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ No monkey found with the name '{name}'.");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n💡 Available monkeys:");
            var allMonkeys = MonkeyHelper.GetAllMonkeys();
            foreach (var m in allMonkeys)
            {
                Console.WriteLine($"   • {m.Name}");
            }
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Gets and displays a random monkey.
    /// </summary>
    static void GetRandomMonkey()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🎲 Random Monkey Selection:");
        Console.WriteLine("===========================");
        Console.ResetColor();
        
        var monkey = MonkeyHelper.GetRandomMonkey();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🎉 Here's your random monkey:");
        Console.WriteLine("=============================");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"🐒 {monkey.Name} ({monkey.Species})");
        Console.ResetColor();
        Console.WriteLine($"📍 Location: {monkey.Location}");
        Console.WriteLine($"👥 Population: {monkey.Population:N0}");
        Console.WriteLine($"📝 {monkey.Description}");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n🌟 Fun fact: Each monkey species has unique characteristics that make them special!");
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the goodbye message.
    /// </summary>
    static void ShowGoodbyeMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒");
        Console.WriteLine("🐒       Thank you for using the       🐒");
        Console.WriteLine("🐒        Monkey Console App!          🐒");
        Console.WriteLine("🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒🐒");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n       🐵 Goodbye! 🐵");
        Console.WriteLine("    Keep monkeying around!");
        Console.ResetColor();
        Console.WriteLine();
    }
}
