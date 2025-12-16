using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class ViewAnalyticsAction : IMenuAction
{
    private readonly IAnalyticsService _analyticsService;

    public ViewAnalyticsAction(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public string Name => "View Analytics Dashboard";

    public void Execute(MenuSession session)
    {
        _analyticsService.DisplayDashboard();
        ConsoleHelper.PressAnyKeyToContinue();
    }
}
