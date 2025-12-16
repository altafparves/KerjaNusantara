using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class LoginGovernmentAction : IMenuAction
{
    private readonly IUserService _userService;

    public LoginGovernmentAction(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login (by Email)";

    public void Execute(MenuSession session)
    {
        ConsoleHelper.DisplaySection("Login");

        var email = ConsoleHelper.ReadInput("Enter your email",
            input => !string.IsNullOrWhiteSpace(input) && input.Contains("@"),
            "Please enter a valid email address.");
            
        var government = _userService.GetAllGovernments().FirstOrDefault(g => g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (government != null)
        {
            session.CurrentUserId = government.Id;
            ConsoleHelper.DisplaySuccess($"Welcome back, {government.AgencyName}!");
        }
        else
        {
            ConsoleHelper.DisplayError("Email not found. Please register first.");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }
}
