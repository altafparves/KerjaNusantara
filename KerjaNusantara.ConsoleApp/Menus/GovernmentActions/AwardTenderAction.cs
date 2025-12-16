using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class AwardTenderAction : IMenuAction
{
    private readonly ITenderService _tenderService;

    public AwardTenderAction(ITenderService tenderService)
    {
        _tenderService = tenderService;
    }

    public string Name => "Award Tender";

    public void Execute(MenuSession session)
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
}
