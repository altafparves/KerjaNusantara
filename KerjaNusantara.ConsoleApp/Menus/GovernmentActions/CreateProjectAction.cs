using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class CreateProjectAction : IMenuAction
{
    private readonly ITenderService _tenderService;

    public CreateProjectAction(ITenderService tenderService)
    {
        _tenderService = tenderService;
    }

    public string Name => "Create New Project";

    public void Execute(MenuSession session)
    {
        ConsoleHelper.DisplaySection("Create New Government Project");

        if (session.CurrentUserId == null)
        {
             ConsoleHelper.DisplayError("User not logged in.");
             return;
        }

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

            var project = _tenderService.CreateProject(session.CurrentUserId, title, description, budget, closingDate);
            ConsoleHelper.DisplaySuccess($"Project '{project.Title}' created successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Error: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }
}
