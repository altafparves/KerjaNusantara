using KerjaNusantara.Domain.Models.Skills;
using KerjaNusantara.Domain.Models.Employment;

namespace KerjaNusantara.Domain.Models.Users;

public class Citizen : User
{
    public string NIK { get; set; } = string.Empty; // National Identity Number
    public SkillProfile SkillProfile { get; set; } = new();
    public List<JobApplication> Applications { get; set; } = new();
    public decimal Balance { get; set; } = 0;
    public int YearsOfExperience { get; set; } = 0;
    public string Location { get; set; } = string.Empty;

    public override void DisplayDashboard()
    {
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine("║        CITIZEN DASHBOARD               ║");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine($"║ Name: {Name,-32} ║");
        Console.WriteLine($"║ NIK: {NIK,-33} ║");
        Console.WriteLine($"║ Balance: Rp {Balance,-26:N0} ║");
        Console.WriteLine($"║ Skills: {SkillProfile.Skills.Count,-29} ║");
        Console.WriteLine($"║ Applications: {Applications.Count,-24} ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }
}
