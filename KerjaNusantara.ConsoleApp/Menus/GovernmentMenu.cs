using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Menus.GovernmentActions;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus;

/// <summary>
/// Government portal menu (Refactored to Command Pattern)
/// </summary>
public class GovernmentMenu
{
    private readonly MenuSession _session;
    private readonly List<IMenuAction> _loginActions;
    private readonly List<IMenuAction> _mainActions;
    private readonly IUserService _userService;

    public GovernmentMenu(IUserService userService, ITenderService tenderService, IAnalyticsService analyticsService)
    {
        _userService = userService;
        _session = new MenuSession();

        // Initialize actions
        _loginActions = new List<IMenuAction>
        {
            new RegisterGovernmentAction(userService),
            new LoginGovernmentAction(userService)
        };

        _mainActions = new List<IMenuAction>
        {
            new CreateProjectAction(tenderService),
            new ViewMyProjectsAction(tenderService),
            new ViewBidsAction(tenderService),
            new AwardTenderAction(tenderService),
            new ViewAnalyticsAction(analyticsService)
        };
    }

    public void Show()
    {
        bool shouldExit = false;
        
        while (!shouldExit)
        {
            ConsoleHelper.DisplayHeader("GOVERNMENT PORTAL");

            if (_session.CurrentUserId == null)
            {
                shouldExit = ShowLoginMenu();
            }
            else
            {
                // Verify user still exists
                var government = _userService.GetGovernmentById(_session.CurrentUserId);
                if (government == null)
                {
                    _session.Clear();
                    shouldExit = ShowLoginMenu();
                }
                else
                {
                    government.DisplayDashboard();
                    shouldExit = ShowMainMenu();
                }
            }
        }
    }

    private bool ShowLoginMenu()
    {
        // Display actions
        for (int i = 0; i < _loginActions.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_loginActions[i].Name}");
        }
        Console.WriteLine($"  {_loginActions.Count + 1}. Back to Main Menu");
        Console.WriteLine();

        var choice = ConsoleHelper.GetMenuChoice(_loginActions.Count + 1);

        if (choice <= _loginActions.Count)
        {
            // Execute login action
            _loginActions[choice - 1].Execute(_session);
            return false;
        }
        else
        {
            // Back
            return true;
        }
    }

    private bool ShowMainMenu()
    {
        // Display available actions
        for (int i = 0; i < _mainActions.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_mainActions[i].Name}");
        }
        Console.WriteLine($"  {_mainActions.Count + 1}. Logout");
        Console.WriteLine();

        var choice = ConsoleHelper.GetMenuChoice(_mainActions.Count + 1);

        if (choice <= _mainActions.Count)
        {
            // Execute action
            _mainActions[choice - 1].Execute(_session);
            return false;
        }
        else
        {
            // Logout
            _session.Clear();
            ConsoleHelper.DisplaySuccess("Logged out successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
            return false; // Return to login menu loop (don't exit GovernmentMenu completely)
        }
    }
}
