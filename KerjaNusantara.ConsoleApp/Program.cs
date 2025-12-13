using KerjaNusantara.ConsoleApp.Configuration;
using KerjaNusantara.ConsoleApp.Menus;

// Configure Dependency Injection
var serviceProvider = ServiceConfiguration.ConfigureServices();

// Start the application
var mainMenu = new MainMenu(serviceProvider);
mainMenu.Show();
