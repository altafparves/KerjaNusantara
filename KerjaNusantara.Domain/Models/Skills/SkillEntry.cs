using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Skills;

public class SkillEntry
{
    public string SkillName { get; set; } = string.Empty;
    public SkillLevel Level { get; set; } = SkillLevel.Beginner;
    public int YearsOfExperience { get; set; } = 0;
}
