using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Skills;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.ConsoleApp.Menus;

/// <summary>
/// Company portal menu
/// </summary>
public class CompanyMenu
{
    private readonly IUserService _userService;
    private readonly IJobService _jobService;
    private readonly ITenderService _tenderService;
    private string? _currentCompanyId;

    public CompanyMenu(IUserService userService, IJobService jobService, ITenderService tenderService)
    {
        _userService = userService;
        _jobService = jobService;
        _tenderService = tenderService;
    }

    public void Show()
    {
        bool shouldExit = false;
        
        while (!shouldExit)
        {
            ConsoleHelper.DisplayHeader("COMPANY PORTAL");

            if (_currentCompanyId == null)
            {
                shouldExit = ShowLoginMenu();
            }
            else
            {
                shouldExit = ShowMainMenu();
            }
        }
    }

    private bool ShowLoginMenu()
    {
        var options = new[] { "Register New Company", "Login (by Email)", "Back to Main Menu" };
        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                RegisterCompany();
                break;
            case 2:
                Login();
                break;
            case 3:
                return true; // Exit to main menu
        }
        
        return false; // Continue in this menu
    }

    private bool ShowMainMenu()
    {
        var company = _userService.GetCompanyById(_currentCompanyId!);
        if (company == null)
        {
            _currentCompanyId = null;
            return true;
        }

        company.DisplayDashboard();

        var options = new[] {
            "Post New Job",
            "View My Jobs",
            "View Applications",
            "Browse Government Tenders",
            "My Tender Bids",
            "Logout"
        };

        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                PostJob();
                break;
            case 2:
                ViewMyJobs();
                break;
            case 3:
                ViewApplications();
                break;
            case 4:
                BrowseTenders();
                break;
            case 5:
                ViewMyBids();
                break;
            case 6:
                _currentCompanyId = null;
                ConsoleHelper.DisplaySuccess("Logged out successfully!");
                ConsoleHelper.PressAnyKeyToContinue();
                return true; // Exit to main menu
        }
        
        return false; // Continue in this menu
    }

    private void RegisterCompany()
    {
        ConsoleHelper.DisplaySection("Register New Company");

        try
        {
            var name = ConsoleHelper.ReadInput("Contact Person Name");
            var email = ConsoleHelper.ReadInput("Email");
            var companyName = ConsoleHelper.ReadInput("Company Name");
            var regNumber = ConsoleHelper.ReadInput("Registration Number");
            var industry = ConsoleHelper.ReadInput("Industry");

            var company = _userService.RegisterCompany(name, email, companyName, regNumber, industry);
            _currentCompanyId = company.Id;

            ConsoleHelper.DisplaySuccess($"Registration successful! Welcome, {company.CompanyName}!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Registration failed: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void Login()
    {
        ConsoleHelper.DisplaySection("Login");

        var email = ConsoleHelper.ReadInput("Enter your email");
        var company = _userService.GetAllCompanies().FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (company != null)
        {
            _currentCompanyId = company.Id;
            ConsoleHelper.DisplaySuccess($"Welcome back, {company.CompanyName}!");
        }
        else
        {
            ConsoleHelper.DisplayError("Email not found. Please register first.");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void PostJob()
    {
        ConsoleHelper.DisplaySection("Post New Job");

        try
        {
            var title = ConsoleHelper.ReadInput("Job Title");
            var description = ConsoleHelper.ReadInput("Description");
            var salary = ConsoleHelper.ReadDecimal("Salary (Rp)");
            var location = ConsoleHelper.ReadInput("Location");
            var minExp = ConsoleHelper.ReadInt("Minimum Years of Experience", 0);

            var requirements = new List<SkillRequirement>();
            Console.WriteLine("\nAdd required skills (enter empty skill name to finish):");
            
            while (true)
            {
                var skillName = ConsoleHelper.ReadInput("Skill Name");
                if (string.IsNullOrWhiteSpace(skillName)) break;

                Console.WriteLine("Level: 0=Beginner, 1=Intermediate, 2=Advanced, 3=Expert");
                var level = (SkillLevel)ConsoleHelper.ReadInt("Minimum Level", 0);

                requirements.Add(new SkillRequirement
                {
                    SkillName = skillName,
                    MinimumLevel = level,
                    IsRequired = true
                });
            }

            var job = _jobService.CreateJob(_currentCompanyId!, title, description, salary, location, minExp, requirements);
            ConsoleHelper.DisplaySuccess($"Job '{job.Title}' posted successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void ViewMyJobs()
    {
        ConsoleHelper.DisplaySection("My Job Postings");

        var jobs = _jobService.GetJobsByCompany(_currentCompanyId!).ToList();

        if (!jobs.Any())
        {
            ConsoleHelper.DisplayWarning("No jobs posted yet.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        foreach (var job in jobs)
        {
            Console.WriteLine($"• {job.Title}");
            Console.WriteLine($"  Salary: Rp {job.Salary:N0} | Location: {job.Location} | Status: {job.Status}");
            Console.WriteLine($"  Posted: {job.PostedDate:yyyy-MM-dd}");
            Console.WriteLine();
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewApplications()
    {
        ConsoleHelper.DisplaySection("Job Applications");

        var jobs = _jobService.GetJobsByCompany(_currentCompanyId!).ToList();
        
        foreach (var job in jobs)
        {
            var applications = _jobService.GetApplicationsByJob(job.Id).ToList();
            if (applications.Any())
            {
                Console.WriteLine($"\n{job.Title}:");
                foreach (var app in applications)
                {
                    Console.WriteLine($"  • {app.CitizenName} - Match: {app.MatchScore}% - Status: {app.Status}");
                }
            }
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void BrowseTenders()
    {
        ConsoleHelper.DisplaySection("Government Tenders");

        var tenders = _tenderService.GetOpenTenders().ToList();

        if (!tenders.Any())
        {
            ConsoleHelper.DisplayWarning("No open tenders available.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        for (int i = 0; i < tenders.Count; i++)
        {
            var tender = tenders[i];
            Console.WriteLine($"{i + 1}. {tender.Title}");
            Console.WriteLine($"   Budget: Rp {tender.Budget:N0} | Agency: {tender.AgencyName}");
            Console.WriteLine($"   {tender.Description}");
            Console.WriteLine();
        }

        var choice = ConsoleHelper.ReadInt("Enter tender number to bid (or 0 to go back)", 0);

        if (choice > 0 && choice <= tenders.Count)
        {
            SubmitBid(tenders[choice - 1].Id);
        }
    }

    private void SubmitBid(string projectId)
    {
        try
        {
            if (_tenderService.HasBid(_currentCompanyId!, projectId))
            {
                ConsoleHelper.DisplayWarning("You have already submitted a bid for this project.");
                ConsoleHelper.PressAnyKeyToContinue();
                return;
            }

            var bidAmount = ConsoleHelper.ReadDecimal("Bid Amount (Rp)");
            var proposal = ConsoleHelper.ReadInput("Proposal Summary");
            var estimatedDays = ConsoleHelper.ReadInt("Estimated Days to Complete");

            var bid = _tenderService.SubmitBid(_currentCompanyId!, projectId, bidAmount, proposal, estimatedDays);
            ConsoleHelper.DisplaySuccess("Bid submitted successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void ViewMyBids()
    {
        ConsoleHelper.DisplaySection("My Tender Bids");

        var bids = _tenderService.GetBidsByCompany(_currentCompanyId!).ToList();

        if (!bids.Any())
        {
            ConsoleHelper.DisplayWarning("No bids submitted yet.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        foreach (var bid in bids)
        {
            var project = _tenderService.GetProjectById(bid.ProjectId);
            Console.WriteLine($"• {project?.Title ?? "Unknown Project"}");
            Console.WriteLine($"  Bid Amount: Rp {bid.BidAmount:N0} | Estimated Days: {bid.EstimatedDays}");
            Console.WriteLine($"  Winner: {(bid.IsWinner ? "YES ✓" : "Pending")}");
            Console.WriteLine();
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }
}
