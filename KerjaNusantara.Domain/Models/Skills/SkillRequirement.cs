using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Skills;

public class SkillRequirement
{
    public string SkillName { get; set; } = string.Empty;
    public SkillLevel MinimumLevel { get; set; } = SkillLevel.Beginner;
    public bool IsRequired { get; set; } = true; // Required vs. Preferred
}
