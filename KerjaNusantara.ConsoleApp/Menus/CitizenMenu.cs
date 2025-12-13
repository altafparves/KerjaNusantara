using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Skills;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.ConsoleApp.Menus;

/// <summary>
/// Citizen portal menu
/// </summary>
public class CitizenMenu
{
    private readonly IUserService _userService;
    private readonly IJobService _jobService;
    private readonly IMatchingService _matchingService;
    private readonly IPaymentService _paymentService;
    private string? _currentCitizenId;

    public CitizenMenu(IUserService userService, IJobService jobService, IMatchingService matchingService, IPaymentService paymentService)
    {
        _userService = userService;
        _jobService = jobService;
        _matchingService = matchingService;
        _paymentService = paymentService;
    }

    public void Show()
    {
        bool shouldExit = false;
        
        while (!shouldExit)
        {
            ConsoleHelper.DisplayHeader("CITIZEN PORTAL");

            if (_currentCitizenId == null)
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
        var options = new[] { "Register New Citizen", "Login (by NIK)", "Back to Main Menu" };
        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                RegisterCitizen();
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
        var citizen = _userService.GetCitizenById(_currentCitizenId!);
        if (citizen == null)
        {
            _currentCitizenId = null;
            return true;
        }

        citizen.DisplayDashboard();

        var options = new[] {
            "View Job Recommendations",
            "Browse All Jobs",
            "My Applications",
            "Manage Skills",
            "View Payment History",
            "Logout"
        };

        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                ViewRecommendations();
                break;
            case 2:
                BrowseJobs();
                break;
            case 3:
                ViewApplications();
                break;
            case 4:
                ManageSkills();
                break;
            case 5:
                ViewPayments();
                break;
            case 6:
                _currentCitizenId = null;
                ConsoleHelper.DisplaySuccess("Logged out successfully!");
                ConsoleHelper.PressAnyKeyToContinue();
                return true; // Exit to main menu
        }
        
        return false; // Continue in this menu
    }

    private void RegisterCitizen()
    {
        ConsoleHelper.DisplaySection("Register New Citizen");

        try
        {
            var name = ConsoleHelper.ReadInput("Full Name");
            var email = ConsoleHelper.ReadInput("Email");
            var nik = ConsoleHelper.ReadInput("NIK (16 digits)");
            var location = ConsoleHelper.ReadInput("Location");

            var citizen = _userService.RegisterCitizen(name, email, nik, location);
            _currentCitizenId = citizen.Id;

            ConsoleHelper.DisplaySuccess($"Registration successful! Welcome, {citizen.Name}!");
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

        var nik = ConsoleHelper.ReadInput("Enter your NIK");
        var citizen = _userService.GetCitizenByNIK(nik);

        if (citizen != null)
        {
            _currentCitizenId = citizen.Id;
            ConsoleHelper.DisplaySuccess($"Welcome back, {citizen.Name}!");
        }
        else
        {
            ConsoleHelper.DisplayError("NIK not found. Please register first.");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewRecommendations()
    {
        ConsoleHelper.DisplaySection("AI Job Recommendations");

        try
        {
            var recommendations = _matchingService.GetRecommendations(_currentCitizenId!, 10);

            if (!recommendations.Any())
            {
                ConsoleHelper.DisplayWarning("No job recommendations available at the moment.");
                ConsoleHelper.PressAnyKeyToContinue();
                return;
            }

            Console.WriteLine($"Found {recommendations.Count} recommended jobs:\n");

            for (int i = 0; i < recommendations.Count; i++)
            {
                var match = recommendations[i];
                Console.WriteLine($"{i + 1}. {match.Job.Title} at {match.Job.CompanyName}");
                Console.WriteLine($"   Match Score: {match.MatchScore}% - {match.GetMatchLevel()}");
                Console.WriteLine($"   Salary: Rp {match.Job.Salary:N0} | Location: {match.Job.Location}");
                Console.WriteLine($"   Recommendation: {match.Recommendation}");
                Console.WriteLine();
            }

            Console.Write("Enter job number to apply (or 0 to go back): ");
            var choice = ConsoleHelper.ReadInt("", 0);

            if (choice > 0 && choice <= recommendations.Count)
            {
                ApplyToJob(recommendations[choice - 1].Job.Id);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void BrowseJobs()
    {
        ConsoleHelper.DisplaySection("Browse All Open Jobs");

        var jobs = _jobService.GetOpenJobs().ToList();

        if (!jobs.Any())
        {
            ConsoleHelper.DisplayWarning("No open jobs available.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        for (int i = 0; i < jobs.Count; i++)
        {
            var job = jobs[i];
            Console.WriteLine($"{i + 1}. {job.Title} at {job.CompanyName}");
            Console.WriteLine($"   Salary: Rp {job.Salary:N0} | Location: {job.Location}");
            Console.WriteLine($"   Required Skills: {string.Join(", ", job.Requirements.Select(r => r.SkillName))}");
            Console.WriteLine();
        }

        var choice = ConsoleHelper.ReadInt("Enter job number to apply (or 0 to go back)", 0);

        if (choice > 0 && choice <= jobs.Count)
        {
            ApplyToJob(jobs[choice - 1].Id);
        }
    }

    private void ApplyToJob(string jobId)
    {
        try
        {
            if (_jobService.HasApplied(_currentCitizenId!, jobId))
            {
                ConsoleHelper.DisplayWarning("You have already applied to this job.");
                ConsoleHelper.PressAnyKeyToContinue();
                return;
            }

            var coverLetter = ConsoleHelper.ReadInput("Cover Letter (optional)");
            var application = _jobService.ApplyToJob(_currentCitizenId!, jobId, coverLetter);

            ConsoleHelper.DisplaySuccess($"Application submitted! Match Score: {application.MatchScore}%");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Application failed: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void ViewApplications()
    {
        ConsoleHelper.DisplaySection("My Applications");

        var applications = _jobService.GetApplicationsByCitizen(_currentCitizenId!).ToList();

        if (!applications.Any())
        {
            ConsoleHelper.DisplayWarning("You haven't applied to any jobs yet.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        foreach (var app in applications)
        {
            var job = _jobService.GetJobById(app.JobId);
            Console.WriteLine($"• {job?.Title ?? "Unknown Job"}");
            Console.WriteLine($"  Status: {app.Status} | Match Score: {app.MatchScore}%");
            Console.WriteLine($"  Applied: {app.AppliedDate:yyyy-MM-dd}");
            Console.WriteLine();
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ManageSkills()
    {
        ConsoleHelper.DisplaySection("Manage Skills");

        var citizen = _userService.GetCitizenById(_currentCitizenId!);
        if (citizen == null) return;

        Console.WriteLine("Current Skills:");
        foreach (var skill in citizen.SkillProfile.Skills)
        {
            Console.WriteLine($"  • {skill.SkillName} - {skill.Level} ({skill.YearsOfExperience} years)");
        }
        Console.WriteLine();

        var options = new[] { "Add New Skill", "Remove Skill", "Back" };
        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                AddSkill(citizen);
                break;
            case 2:
                RemoveSkill(citizen);
                break;
        }
    }

    private void AddSkill(Domain.Models.Users.Citizen citizen)
    {
        var skillName = ConsoleHelper.ReadInput("Skill Name (e.g., C#, Java, Python)");
        Console.WriteLine("Skill Level: 0=Beginner, 1=Intermediate, 2=Advanced, 3=Expert");
        var level = (SkillLevel)ConsoleHelper.ReadInt("Level", 0);
        var years = ConsoleHelper.ReadInt("Years of Experience", 0);

        citizen.SkillProfile.AddSkill(new SkillEntry
        {
            SkillName = skillName,
            Level = level,
            YearsOfExperience = years
        });

        _userService.UpdateCitizen(citizen);
        ConsoleHelper.DisplaySuccess("Skill added successfully!");
        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void RemoveSkill(Domain.Models.Users.Citizen citizen)
    {
        var skillName = ConsoleHelper.ReadInput("Skill Name to Remove");
        citizen.SkillProfile.RemoveSkill(skillName);
        _userService.UpdateCitizen(citizen);
        ConsoleHelper.DisplaySuccess("Skill removed!");
        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewPayments()
    {
        ConsoleHelper.DisplaySection("Payment History");

        var payments = _paymentService.GetPaymentsByCitizen(_currentCitizenId!).ToList();

        if (!payments.Any())
        {
            ConsoleHelper.DisplayWarning("No payment history yet.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        var totalEarnings = _paymentService.GetTotalEarnings(_currentCitizenId!);
        Console.WriteLine($"Total Earnings: Rp {totalEarnings:N0}\n");

        foreach (var payment in payments)
        {
            Console.WriteLine($"• {payment.JobTitle}");
            Console.WriteLine($"  Amount: Rp {payment.Amount:N0} | Status: {payment.Status}");
            Console.WriteLine($"  Date: {payment.CreatedDate:yyyy-MM-dd}");
            Console.WriteLine();
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }
}
