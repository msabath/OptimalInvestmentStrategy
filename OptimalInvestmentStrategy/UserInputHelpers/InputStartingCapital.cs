using System.Globalization;
using Sharprompt;
using Spectre.Console;

namespace OptimalInvestmentStrategy.UserInputHelpers;

public static class InputStartingCapital
{
    private static readonly Random Random = new Random();
    private static int _startingCapital = 0;
    
    private const int StartingCapitalMinimum = 1;
    private const int StartingCapitalMaximum = 1000000000;

    private const string StartingCapitalEntryMessage = "Please enter your [green]starting capital[/] or enter 0 to choose a random value: ";

    private static readonly string StartingCapitalOutOfBoundsMessage =
        $"Starting capital must be between ${StartingCapitalMinimum:n0} and ${StartingCapitalMaximum:n0}";


    public static int Execute()
    {
        bool success = false;

        while (!success)
        {
            var input = AnsiConsole.Ask<string>(StartingCapitalEntryMessage);
            
            // Convert user input to an Int.
            // TryParse to deal with nulls
            // NumberStyles.Any and providing CultureInfo allows for the easy conversion to Int
            // by removing with commas, dollar signs, and formatting issues
            //      
            if (Int32.TryParse(input, NumberStyles.Any, new CultureInfo("en-US"), out var startingCapital))
            {
                if (startingCapital == 0)
                {
                    Console.WriteLine("You have chosen a random value for your starting capital.");
                    _startingCapital = Random.Next(StartingCapitalMinimum, StartingCapitalMaximum);
                    success = true;
                    continue;
                }
                
                if (startingCapital is < StartingCapitalMinimum or > StartingCapitalMaximum)
                {
                    Console.WriteLine(StartingCapitalOutOfBoundsMessage);
                    continue;
                }

                _startingCapital = startingCapital;
                success = true;
            }
            
        }
        
        
        Console.WriteLine($"Your starting capital is: ${_startingCapital:n0}");
        return _startingCapital;
    }
}