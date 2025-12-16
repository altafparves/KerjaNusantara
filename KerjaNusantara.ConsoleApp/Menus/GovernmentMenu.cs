using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus;

/// <summary>
/// Government portal menu
/// </summary>
public class GovernmentMenu
{
    private readonly IUserService _userService;
    private readonly ITenderService _tenderService;
    private readonly IAnalyticsService _analyticsService;
    private string? _currentGovernmentId;

    public GovernmentMenu(IUserService userService, ITenderService tenderService, IAnalyticsService analyticsService)
    {
        _userService = userService;
        _tenderService = tenderService;
        _analyticsService = analyticsService;
    }

    public void Show()
    {
        bool shouldExit = false;
        
        while (!shouldExit)
        {
            ConsoleHelper.DisplayHeader("GOVERNMENT PORTAL");

            if (_currentGovernmentId == null)
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
        var options = new[] { "Register New Government Entity", "Login (by Email)", "Back to Main Menu" };
        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                RegisterGovernment();
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
        var government = _userService.GetGovernmentById(_currentGovernmentId!);
        if (government == null)
        {
            _currentGovernmentId = null;
            return true;
        }

        government.DisplayDashboard();

        var options = new[] {
            "Create New Project",
            "View My Projects",
            "View Project Bids",
            "Award Tender",
            "View Analytics Dashboard",
            "Logout"
        };

        ConsoleHelper.DisplayMenu(options);
        var choice = ConsoleHelper.GetMenuChoice(options.Length);

        switch (choice)
        {
            case 1:
                CreateProject();
                break;
            case 2:
                ViewMyProjects();
                break;
            case 3:
                ViewBids();
                break;
            case 4:
                AwardTender();
                break;
            case 5:
                ViewAnalytics();
                break;
            case 6:
                _currentGovernmentId = null;
                ConsoleHelper.DisplaySuccess("Logged out successfully!");
                ConsoleHelper.PressAnyKeyToContinue();
                return true; // Exit to main menu
        }
        
        return false; // Continue in this menu
    }

    private void RegisterGovernment()
    {
        ConsoleHelper.DisplaySection("Register Government Entity");

        try
        {
            var name = ConsoleHelper.ReadInput("Contact Person Name",
                input => !string.IsNullOrWhiteSpace(input) && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)),
                "Name must contain only letters and cannot be empty.");
            
            var email = ConsoleHelper.ReadInput("Email",
                input => !string.IsNullOrWhiteSpace(input) && input.Contains("@"),
                "Please enter a valid email address.");

            var agencyName = ConsoleHelper.ReadInput("Agency Name",
                input => !string.IsNullOrWhiteSpace(input),
                "Agency Name cannot be empty.");

            var department = ConsoleHelper.ReadInput("Department",
                 input => !string.IsNullOrWhiteSpace(input),
                "Department cannot be empty.");

            var government = _userService.RegisterGovernment(name, email, agencyName, department);
            _currentGovernmentId = government.Id;

            ConsoleHelper.DisplaySuccess($"Registration successful! Welcome, {government.AgencyName}!");
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

        var email = ConsoleHelper.ReadInput("Enter your email",
            input => !string.IsNullOrWhiteSpace(input) && input.Contains("@"),
            "Please enter a valid email address.");
            
        var government = _userService.GetAllGovernments().FirstOrDefault(g => g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (government != null)
        {
            _currentGovernmentId = government.Id;
            ConsoleHelper.DisplaySuccess($"Welcome back, {government.AgencyName}!");
        }
        else
        {
            ConsoleHelper.DisplayError("Email not found. Please register first.");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CreateProject()
    {
        ConsoleHelper.DisplaySection("Create New Government Project");

        try
        {
            var title = ConsoleHelper.ReadInput("Project Title",
                input => !string.IsNullOrWhiteSpace(input),
                "Project Title cannot be empty.");
                
            var description = ConsoleHelper.ReadInput("Description",
                input => !string.IsNullOrWhiteSpace(input) && input.Length > 20,
                "Description must be at least 20 characters.");
                
            var budget = ConsoleHelper.ReadDecimal("Budget (Rp)");
            
            DateTime? closingDate = null;
            var dateStr = ConsoleHelper.ReadInput("Tender Closing Date (yyyy-MM-dd, or Enter to skip)", 
                input => string.IsNullOrWhiteSpace(input) || DateTime.TryParse(input, out _),
                "Invalid date format. Please use yyyy-MM-dd.");
            
            if (!string.IsNullOrWhiteSpace(dateStr))
            {
                closingDate = DateTime.Parse(dateStr);
            }

            var project = _tenderService.CreateProject(_currentGovernmentId!, title, description, budget, closingDate);
            ConsoleHelper.DisplaySuccess($"Project '{project.Title}' created successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void ViewMyProjects()
    {
        ConsoleHelper.DisplaySection("My Projects");

        var projects = _tenderService.GetProjectsByGovernment(_currentGovernmentId!).ToList();

        if (!projects.Any())
        {
            ConsoleHelper.DisplayWarning("No projects created yet.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        foreach (var project in projects)
        {
            Console.WriteLine($"• {project.Title}");
            Console.WriteLine($"  Budget: Rp {project.Budget:N0} | Status: {project.Status}");
            if (project.AwardedToCompanyName != null)
            {
                Console.WriteLine($"  Awarded to: {project.AwardedToCompanyName}");
            }
            Console.WriteLine();
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewBids()
    {
        ConsoleHelper.DisplaySection("Project Bids");

        var projects = _tenderService.GetProjectsByGovernment(_currentGovernmentId!).ToList();

        foreach (var project in projects)
        {
            var bids = _tenderService.GetBidsByProject(project.Id).ToList();
            if (bids.Any())
            {
                Console.WriteLine($"\n{project.Title}:");
                foreach (var bid in bids)
                {
                    Console.WriteLine($"  • {bid.CompanyName}");
                    Console.WriteLine($"    Bid: Rp {bid.BidAmount:N0} | Days: {bid.EstimatedDays} | Winner: {(bid.IsWinner ? "YES" : "No")}");
                }
            }
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AwardTender()
    {
        ConsoleHelper.DisplaySection("Award Tender");

        var projects = _tenderService.GetOpenTenders().ToList();

        if (!projects.Any())
        {
            ConsoleHelper.DisplayWarning("No open tenders to award.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {projects[i].Title}");
        }

        var projectChoice = ConsoleHelper.ReadInt("\nEnter project number", 0);
        if (projectChoice < 1 || projectChoice > projects.Count) return;

        var selectedProject = projects[projectChoice - 1];
        var bids = _tenderService.GetBidsByProject(selectedProject.Id).ToList();

        if (!bids.Any())
        {
            ConsoleHelper.DisplayWarning("No bids for this project.");
            ConsoleHelper.PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("\nBids:");
        for (int i = 0; i < bids.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {bids[i].CompanyName} - Rp {bids[i].BidAmount:N0}");
        }

        var bidChoice = ConsoleHelper.ReadInt("\nEnter winning bid number", 0);
        if (bidChoice < 1 || bidChoice > bids.Count) return;

        try
        {
            _tenderService.AwardTender(selectedProject.Id, bids[bidChoice - 1].Id);
            ConsoleHelper.DisplaySuccess($"Tender awarded to {bids[bidChoice - 1].CompanyName}!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void ViewAnalytics()
    {
        _analyticsService.DisplayDashboard();
        ConsoleHelper.PressAnyKeyToContinue();
    }
}
