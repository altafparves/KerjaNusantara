using KerjaNusantara.ConsoleApp.Utilities;
using Microsoft.Extensions.DependencyInjection;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus;

/// <summary>
/// Main menu - entry point for the application
/// </summary>
public class MainMenu
{
    private readonly IServiceProvider _serviceProvider;

    public MainMenu(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.DisplayHeader("KERJANUSANTARA - Employment Management System");

            Console.WriteLine("Welcome to KerjaNusantara!");
            Console.WriteLine("Connecting Citizens, Companies, and Government\n");

            var options = new[] {
                "Citizen Portal",
                "Company Portal",
                "Government Portal",
                "Exit"
            };

            ConsoleHelper.DisplayMenu(options);
            var choice = ConsoleHelper.GetMenuChoice(options.Length);

            switch (choice)
            {
                case 1:
                    ShowCitizenPortal();
                    break;
                case 2:
                    ShowCompanyPortal();
                    break;
                case 3:
                    ShowGovernmentPortal();
                    break;
                case 4:
                    ConsoleHelper.DisplaySuccess("Thank you for using KerjaNusantara!");
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void ShowCitizenPortal()
    {
        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var jobService = _serviceProvider.GetRequiredService<IJobService>();
        var matchingService = _serviceProvider.GetRequiredService<IMatchingService>();
        var paymentService = _serviceProvider.GetRequiredService<IPaymentService>();

        var menu = new CitizenMenu(userService, jobService, matchingService, paymentService);
        menu.Show();
    }

    private void ShowCompanyPortal()
    {
        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var jobService = _serviceProvider.GetRequiredService<IJobService>();
        var tenderService = _serviceProvider.GetRequiredService<ITenderService>();

        var menu = new CompanyMenu(userService, jobService, tenderService);
        menu.Show();
    }

    private void ShowGovernmentPortal()
    {
        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var tenderService = _serviceProvider.GetRequiredService<ITenderService>();
        var analyticsService = _serviceProvider.GetRequiredService<IAnalyticsService>();

        var menu = new GovernmentMenu(userService, tenderService, analyticsService);
        menu.Show();
    }
}
