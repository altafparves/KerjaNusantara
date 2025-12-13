using KerjaNusantara.Domain.Interfaces;

namespace KerjaNusantara.Domain.Models.Users;

public abstract class User : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Template Method Pattern - each user type implements their own dashboard
    public abstract void DisplayDashboard();
}
