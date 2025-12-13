using KerjaNusantara.Domain.Interfaces;

namespace KerjaNusantara.Domain.Models.Skills;

public class Skill : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // e.g., "Technical", "Soft Skills", "Language"
    public string Description { get; set; } = string.Empty;
}
