using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Matching;

public class SkillGap
{
    public string RequiredSkillName { get; set; } = string.Empty;
    public SkillLevel RequiredLevel { get; set; } = SkillLevel.Beginner;
    public SkillLevel? CurrentLevel { get; set; } // null if skill not possessed
    public string TrainingRecommendation { get; set; } = string.Empty;

    public bool HasSkill => CurrentLevel.HasValue;
    
    public string GetGapDescription()
    {
        if (!HasSkill)
            return $"Missing skill: {RequiredSkillName} (Required: {RequiredLevel})";
        
        if (CurrentLevel < RequiredLevel)
            return $"Skill gap: {RequiredSkillName} (Current: {CurrentLevel}, Required: {RequiredLevel})";
        
        return $"Skill met: {RequiredSkillName}";
    }
}
