using Sharprompt;

namespace OptimalInvestmentStrategy.DataGenerators;

public static class ProjectGenerator
{
    public static List<Project> GenerateWithTestData()
    {
        var projects = new List<Project>();
      
        projects.Add(new Project(1,1,"1"));
        projects.Add(new Project(3,2,"2"));
        projects.Add(new Project(4,3,"3"));
        projects.Add(new Project(5,4,"4"));
        projects.Add(new Project(6,5,"5"));
        Console.WriteLine("Test projects added!");

        return projects;
    }
    public static List<Project> Generate(int numberOfProjects)
    {
        var projects = new List<Project>();

        var generate = Prompt.Confirm("Would you like to generate random projects?", defaultValue: true);
        
        if (generate)
        {
            Console.WriteLine($"generating projects....");

            for (int x = 0; x < numberOfProjects; x++)
            {
                projects.Add(new Project(x.ToString()));
            }
        }
        else
        {
            for (int x = 0; x < numberOfProjects; x++)
            {
                Console.WriteLine($"Creating project {x+1}...");
                var capital = Prompt.Input<Int32>("Enter the required capital: ");
                var profit = Prompt.Input<Int32>("Enter the projected profit: ");

                var project = new Project(capital, profit, x.ToString());
                projects.Add(project);
                Console.WriteLine("Project Added!");
            }
        }

        Console.WriteLine($"Created {projects.Count} projects!");
       
        return projects;
    }
}