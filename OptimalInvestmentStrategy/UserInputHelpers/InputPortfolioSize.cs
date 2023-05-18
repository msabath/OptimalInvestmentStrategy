using System.Globalization;
using Sharprompt;
using Spectre.Console;

namespace OptimalInvestmentStrategy.UserInputHelpers;

public static class InputPortfolioSize
{
    private static readonly Random Random = new Random();
    
    private static int _portfolioSize = 0;
    private const int PortfolioCountMinimum = 1;
    private const int PortfolioCountMaximum = 100000;
    
    
    private const string PortfolioSizeEntryMessage = "Please enter the [green]portfolio size[/] of available projects or enter 0 to choose a random value:: ";
    private static readonly string PortfolioSizeOutOfBoundsMessage =
        $"Portfolio size must be between {PortfolioCountMinimum:n0} and {PortfolioCountMaximum:n0}";

    public static int Execute()
    {
        bool success = false;

        while (!success)
        {
            var input = AnsiConsole.Ask<string>(PortfolioSizeEntryMessage);
            

            // Convert user input to an Int.
            // TryParse to deal with nulls
            // NumberStyles.Any and providing CultureInfo allows for the easy conversion to Int
            // by removing with commas, dollar signs, and formatting issues
            //      
            if (Int32.TryParse(input, NumberStyles.Any, new CultureInfo("en-US"), out var portfolioSize))
            {
                if (portfolioSize == 0)
                {
                    Console.WriteLine("You have chosen a random value for your portfolio size.");
                    _portfolioSize = Random.Next(PortfolioCountMinimum, PortfolioCountMaximum);
                    success = true;
                    continue;
                }
                
                if (portfolioSize is < PortfolioCountMinimum or > PortfolioCountMaximum)
                {
                    Console.WriteLine(PortfolioSizeOutOfBoundsMessage);
                    continue;
                }

                _portfolioSize = portfolioSize;
                success = true;
            }
            
        }
        
        
        Console.WriteLine($"Your portfolio size is: {_portfolioSize:n0}");
        return _portfolioSize;
    }
    
}