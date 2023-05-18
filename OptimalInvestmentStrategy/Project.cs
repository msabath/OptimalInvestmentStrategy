namespace OptimalInvestmentStrategy;

public class Project
{
    public string Identifier { get; private set; } = "";
    /*
     * Choosing int for simplicity.
     * Obviously, real dollar projections would use the decimal type
     */
    public int CapitalRequired { get; private set; } = 0;
    public int ProjectedProfit { get; private set; } = 0;
    public double ReturnOnInvestedCapital => (double)ProjectedProfit/(double)CapitalRequired;

    public bool IsSelected = false;

    private const int CapitalRequiredMinimum = 1;
    private const int CapitalRequiredMaximum = 1000000000;
    
    private const int ProfitExpectedMinimum = 0;
    private const int ProfitExpectedMaximum = 1000000000;

    public Project(int capitalRequired, int projectedProfit, string identifier)
    {
        if (capitalRequired is < CapitalRequiredMinimum or > CapitalRequiredMaximum)
        {
            throw new ArgumentException($"Capital required must be between ${CapitalRequiredMinimum:n0} and ${CapitalRequiredMaximum:n0}");
        }
        
        if (projectedProfit is < ProfitExpectedMinimum or > ProfitExpectedMaximum)
        {
            throw new ArgumentException($"Projected profit must be between ${ProfitExpectedMinimum:n0} and ${ProfitExpectedMaximum:n0}");
        }
        
        CapitalRequired = capitalRequired;
        ProjectedProfit = projectedProfit;
        Identifier = "P" + identifier;
    }

    public Project()
    {
        var rnd = new Random();
        
        CapitalRequired = rnd.Next(CapitalRequiredMinimum, CapitalRequiredMaximum);
        ProjectedProfit = rnd.Next(ProfitExpectedMinimum, ProfitExpectedMaximum);
        
        //This is bad. I'm not dealing with guaranteed uniqueness in this approach. 
        //Typically would address with either a hash map, DB uniqueness, or some other approach depending on constraints
        Identifier = "P" + rnd.Next(1, 10000000);
    }
    
    public Project(string identifier)
    {
        var rnd = new Random();
        
        CapitalRequired = rnd.Next(CapitalRequiredMinimum, CapitalRequiredMaximum);
        ProjectedProfit = rnd.Next(ProfitExpectedMinimum, ProfitExpectedMaximum);
        Identifier = "P" + identifier;
    }

    public void Print()
    {
        Console.WriteLine($"Capital Required is ${CapitalRequired:n0}");
        Console.WriteLine($"Projected Profit is ${ProjectedProfit:n0}");
    }
}