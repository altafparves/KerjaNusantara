using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Projects;

namespace KerjaNusantara.Domain.Models.Users;

public class Company : User
{
    public string CompanyRegistrationNumber { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public List<Job> PostedJobs { get; set; } = new();
    public List<TenderBid> TenderBids { get; set; } = new();

    public override void DisplayDashboard()
    {
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine("║        COMPANY DASHBOARD               ║");
        Console.WriteLine("╠════════════════════════════════════════╣");
        Console.WriteLine($"║ Company: {CompanyName,-28} ║");
        Console.WriteLine($"║ Industry: {Industry,-27} ║");
        Console.WriteLine($"║ Posted Jobs: {PostedJobs.Count,-24} ║");
        Console.WriteLine($"║ Tender Bids: {TenderBids.Count,-24} ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }
}
