using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class ViewBidsAction : IMenuAction
{
    private readonly ITenderService _tenderService;

    public ViewBidsAction(ITenderService tenderService)
    {
        _tenderService = tenderService;
    }

    public string Name => "View Project Bids";

    public void Execute(MenuSession session)
    {
        ConsoleHelper.DisplaySection("Project Bids");

        if (session.CurrentUserId == null)
        {
             ConsoleHelper.DisplayError("User not logged in.");
             return;
        }

        var projects = _tenderService.GetProjectsByGovernment(session.CurrentUserId).ToList();

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
}
