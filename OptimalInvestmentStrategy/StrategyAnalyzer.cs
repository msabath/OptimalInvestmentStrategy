using System.Text;
using OptimalInvestmentStrategy.DataGenerators;
using OptimalInvestmentStrategy.UserInputHelpers;
using Sharprompt;

namespace OptimalInvestmentStrategy;

public class StrategyAnalyzer
{
    private const string WelcomeMessage = "Welcome to the Optimal Investment Strategy program!";
    private const string PromptUseProvidedTestData = "Would you like to use the provided test data?";
    
    
    public StrategyAnalyzer()
    {
        int numberOfProjects;
        int portfolioSize;
        int initialCapital;
        List<Project> projects;
        
        Console.WriteLine(WelcomeMessage);
        Console.WriteLine("");
        
        var useTestData = Prompt.Confirm(PromptUseProvidedTestData, defaultValue: true);
        if (useTestData)
        {
            Console.WriteLine("Setting initial capital to 2");
            initialCapital = 2;
            Console.WriteLine("Setting portfolio size to 5");
            portfolioSize = 5;
            Console.WriteLine("Setting project limit to 3");
            numberOfProjects = 3;

            projects = ProjectGenerator.GenerateWithTestData();
        }
        else
        {
            initialCapital = InputStartingCapital.Execute();
            portfolioSize = InputPortfolioSize.Execute();
            numberOfProjects = InputNumberOfProjects.Execute();
            
            if (numberOfProjects > portfolioSize)
            {
                Console.WriteLine($"Your project count ({numberOfProjects:n0}) is larger than the size of the portfolio ({portfolioSize:n0}).");
                Console.WriteLine($"Setting your project count to the size of the portfolio.");
                portfolioSize = numberOfProjects;
            }
            
            projects = ProjectGenerator.Generate(portfolioSize)
                .ToList();
        }


        // Creating new list to store select projects in to avoid parsing large collection another time
        // It also allows us to maintain sequence of selection without adding to the data structure
        var selectedProjects = new List<Project>();
        long availableCapital = initialCapital;

        /*
         * 
         *  My approach below is to sort the portfolio by Projected Profit desc and take the first project that I
         *  can afford. This simple approach is effective, but costly in BigO terms. There must be a way to get to O(n)
         *  It requires a resort and reevaluation after every project selection because the Available Capital has
         *  increased. A previous project that was skipped may be applicable.
         *
         *  There may be an opportunity to use some type of tree or linked list data structure so that we can examine
         *  forward and back nodes after applying new capital.
         *
         *  An area that I need to think through further before applying a performance optimization would be the impact
         *  of multiple projects in combination providing more profit at a smaller cumulative investment than a larger
         *  single project.
         *
         *  I also experimented with sorting by the Return on Invested Capital calculation. This is an interesting approach
         *  and may actually yield better results at large project and portfolio counts. It may also be closer to the
         *  needs of an investor, but that is subjective and situational.
         *
         *  I tried to apply a variant of Kadane's algorithm, but this problem doesn't match well, or at least I couldn't
         *  figure it out. We don't care about contiguity in the array of projects.
         *
         *  
         */

        for (int p = 0; p < numberOfProjects; p++)
        {
            var selectedProject = projects
                .Where(x => x.CapitalRequired <= availableCapital && x.IsSelected == false)
                .OrderByDescending(project => project.ProjectedProfit)
                .FirstOrDefault();
            
            if (selectedProject == null)
            {
                Console.WriteLine("No more available projects matching capital requirements.");
                break;
            }

            selectedProject.IsSelected = true;
            availableCapital += selectedProject.ProjectedProfit;
            selectedProjects.Add(selectedProject);
            
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"Added project {selectedProject.Identifier} with capital requirement of {selectedProject.CapitalRequired:n0} and projected profit of {selectedProject.ProjectedProfit:n0}");
            Console.WriteLine($"Available capital is now ${availableCapital:n0}");
        }
        
        Console.WriteLine("----------------------------------------------------------------------------");


        StringBuilder projectsString = new StringBuilder();
        projectsString.Append("Selected Projects:");
        
        StringBuilder withCapitalString = new StringBuilder();
        withCapitalString.Append(" with capital");
        
        StringBuilder profitsString = new StringBuilder();
        profitsString.Append("Selected Profits");
        
        StringBuilder optimalInvestmentStrategyString = new StringBuilder();
        optimalInvestmentStrategyString.Append($"Optimal Investment Strategy returns = {initialCapital}");

        for (int projectIterator = 0; projectIterator < selectedProjects.Count; projectIterator++)
        {
            projectsString.Append(" " + selectedProjects[projectIterator].Identifier);
            withCapitalString.Append(" " +selectedProjects[projectIterator].CapitalRequired);
            profitsString.Append(" " + selectedProjects[projectIterator].ProjectedProfit);
            
            optimalInvestmentStrategyString.Append(" + " + selectedProjects[projectIterator].ProjectedProfit);
        }

        optimalInvestmentStrategyString.Append($" = {availableCapital}");
        
        Console.WriteLine(projectsString.ToString() + withCapitalString.ToString());
        Console.WriteLine(profitsString.ToString());
        Console.WriteLine("");
        Console.WriteLine(optimalInvestmentStrategyString.ToString());
        Console.WriteLine($"Optimal Strategy maximizes your capital to = {availableCapital}");
    }
}
