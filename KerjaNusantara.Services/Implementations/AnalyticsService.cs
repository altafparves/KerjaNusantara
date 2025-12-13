using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// Analytics service implementation
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IUserRepository<Domain.Models.Users.Citizen> _citizenRepo;
    private readonly IUserRepository<Domain.Models.Users.Company> _companyRepo;
    private readonly IJobRepository _jobRepo;
    private readonly IApplicationRepository _applicationRepo;
    private readonly IProjectRepository _projectRepo;
    private readonly ITenderBidRepository _bidRepo;

    public AnalyticsService(
        IUserRepository<Domain.Models.Users.Citizen> citizenRepo,
        IUserRepository<Domain.Models.Users.Company> companyRepo,
        IJobRepository jobRepo,
        IApplicationRepository applicationRepo,
        IProjectRepository projectRepo,
        ITenderBidRepository bidRepo)
    {
        _citizenRepo = citizenRepo;
        _companyRepo = companyRepo;
        _jobRepo = jobRepo;
        _applicationRepo = applicationRepo;
        _projectRepo = projectRepo;
        _bidRepo = bidRepo;
    }

    public int GetTotalCitizens() => _citizenRepo.Count();

    public int GetTotalCompanies() => _companyRepo.Count();

    public int GetTotalJobs() => _jobRepo.Count();

    public int GetTotalOpenJobs() => _jobRepo.GetOpenJobs().Count();

    public int GetTotalApplications() => _applicationRepo.Count();

    public int GetTotalProjects() => _projectRepo.Count();

    public int GetTotalBids() => _bidRepo.Count();

    public double GetEmploymentRate()
    {
        var totalApplications = GetTotalApplications();
        if (totalApplications == 0)
            return 0;

        var acceptedApplications = _applicationRepo.GetByStatus(ApplicationStatus.Accepted).Count();
        return (double)acceptedApplications / totalApplications * 100;
    }

    public void DisplayDashboard()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          EMPLOYMENT ANALYTICS DASHBOARD               ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                        ║");
        Console.WriteLine("║  📊 USER STATISTICS                                    ║");
        Console.WriteLine($"║     Citizens Registered: {GetTotalCitizens(),-28} ║");
        Console.WriteLine($"║     Companies Registered: {GetTotalCompanies(),-27} ║");
        Console.WriteLine("║                                                        ║");
        Console.WriteLine("║  💼 EMPLOYMENT STATISTICS                              ║");
        Console.WriteLine($"║     Total Jobs Posted: {GetTotalJobs(),-30} ║");
        Console.WriteLine($"║     Open Jobs: {GetTotalOpenJobs(),-38} ║");
        Console.WriteLine($"║     Total Applications: {GetTotalApplications(),-29} ║");
        Console.WriteLine($"║     Employment Rate: {GetEmploymentRate():F2}%{new string(' ', 28)} ║");
        Console.WriteLine("║                                                        ║");
        Console.WriteLine("║  🏛️  GOVERNMENT PROJECTS                               ║");
        Console.WriteLine($"║     Total Projects: {GetTotalProjects(),-33} ║");
        Console.WriteLine($"║     Total Bids: {GetTotalBids(),-37} ║");
        Console.WriteLine("║                                                        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }
}
