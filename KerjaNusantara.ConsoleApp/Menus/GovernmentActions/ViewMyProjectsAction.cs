using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class ViewMyProjectsAction : IMenuAction
{
    private readonly ITenderService _tenderService;

    public ViewMyProjectsAction(ITenderService tenderService)
    {
        _tenderService = tenderService;
    }

    public string Name => "View My Projects";

    public void Execute(MenuSession session)
    {
         ConsoleHelper.DisplaySection("My Projects");

        if (session.CurrentUserId == null)
        {
             ConsoleHelper.DisplayError("User not logged in.");
             return;
        }

        var projects = _tenderService.GetProjectsByGovernment(session.CurrentUserId).ToList();

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
}
