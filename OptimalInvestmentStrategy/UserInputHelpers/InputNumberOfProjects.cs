using System.Globalization;
using Sharprompt;
using Spectre.Console;

namespace OptimalInvestmentStrategy.UserInputHelpers;

public static class InputNumberOfProjects
{
    private static readonly Random Random = new Random();
    
    private static int _numberOfProjects = 0;
    
    private const int ProjectCountMinimum = 1;
    private const int ProjectCountMaximum = 100000;
    
    private const string NumberOfProjectsEntryMessage = "Please enter the [green]maximum amount of projects[/] you would like to invest in or enter 0 to choose a random value: ";
    private static readonly string ProjectCountOutOfBoundsMessage =
        $"Project count must be between ${ProjectCountMinimum:n0} and ${ProjectCountMaximum:n0}";

    public static int Execute()
    {
        bool success = false;

        while (!success)
        {
            var input = AnsiConsole.Ask<string>(NumberOfProjectsEntryMessage);
            
            // Convert user input to an Int.
            // TryParse to deal with nulls
            // NumberStyles.Any and providing CultureInfo allows for the easy conversion to Int
            // by removing with commas, dollar signs, and formatting issues
            //      
            if (Int32.TryParse(input, NumberStyles.Any, new CultureInfo("en-US"), out var numberOfProjects))
            {
                if (numberOfProjects == 0)
                {
                    Console.WriteLine("You have chosen a random value for your number of projects.");
                    _numberOfProjects = Random.Next(ProjectCountMinimum, ProjectCountMaximum);
                    success = true;
                    continue;
                }
                
                if (numberOfProjects is < ProjectCountMinimum or > ProjectCountMaximum)
                {
                    Console.WriteLine(ProjectCountOutOfBoundsMessage);
                    continue;
                }

                _numberOfProjects = numberOfProjects;
                success = true;
            }
            
        }
        
        Console.WriteLine($"Your number of projects is: {_numberOfProjects:n0}");
        return _numberOfProjects;
    }
}