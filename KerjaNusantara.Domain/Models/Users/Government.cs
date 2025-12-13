using KerjaNusantara.Domain.Models.Projects;

namespace KerjaNusantara.Domain.Models.Users;

public class Government : User
{
    public string AgencyName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public List<GovernmentProject> Projects { get; set; } = new();

    public override void DisplayDashboard()
    {
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine("║      GOVERNMENT DASHBOARD              ║");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine($"║ Agency: {AgencyName,-29} ║");
        Console.WriteLine($"║ Department: {Department,-25} ║");
        Console.WriteLine($"║ Active Projects: {Projects.Count,-20} ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }
}
