namespace KerjaNusantara.ConsoleApp.Utilities;

/// <summary>
/// Console helper utilities for better UI
/// </summary>
public static class ConsoleHelper
{
    public static void DisplayHeader(string title)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  {title.PadRight(52)}║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    public static void DisplaySection(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"═══ {title} ═══");
        Console.WriteLine();
    }

    public static void DisplaySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ {message}");
        Console.ResetColor();
    }

    public static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ {message}");
        Console.ResetColor();
    }

    public static void DisplayWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠ {message}");
        Console.ResetColor();
    }

    public static void DisplayInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"ℹ {message}");
        Console.ResetColor();
    }

    public static string ReadInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public static string ReadInput(string prompt, Func<string, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine() ?? string.Empty;

            if (validator(input))
            {
                return input;
            }

            DisplayError(errorMessage);
        }
    }

    public static int ReadInt(string prompt, int defaultValue = 0)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();

            // If empty, return default
            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }

            // If valid integer, return it
            if (int.TryParse(input, out var result))
            {
                return result;
            }

            // Otherwise, show error and loop
            DisplayError("Invalid input. Please enter a valid number.");
        }
    }

    public static decimal ReadDecimal(string prompt, decimal defaultValue = 0)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }

            if (decimal.TryParse(input, out var result))
            {
                return result;
            }

            DisplayError("Invalid input. Please enter a valid number.");
        }
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public static void DisplayMenu(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {options[i]}");
        }
        Console.WriteLine();
    }

    public static int GetMenuChoice(int maxOption)
    {
        while (true)
        {
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            
            if (int.TryParse(input, out var choice) && choice >= 1 && choice <= maxOption)
            {
                return choice;
            }
            
            DisplayError($"Invalid choice. Please enter a number between 1 and {maxOption}.");
        }
    }
}
