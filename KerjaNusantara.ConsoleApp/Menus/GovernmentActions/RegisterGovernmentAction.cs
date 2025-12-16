using KerjaNusantara.ConsoleApp.Framework;
using KerjaNusantara.ConsoleApp.Utilities;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.ConsoleApp.Menus.GovernmentActions;

public class RegisterGovernmentAction : IMenuAction
{
    private readonly IUserService _userService;

    public RegisterGovernmentAction(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Register New Government Entity";

    public void Execute(MenuSession session)
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
            session.CurrentUserId = government.Id;

            ConsoleHelper.DisplaySuccess($"Registration successful! Welcome, {government.AgencyName}!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayError($"Registration failed: {ex.Message}");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }
}
