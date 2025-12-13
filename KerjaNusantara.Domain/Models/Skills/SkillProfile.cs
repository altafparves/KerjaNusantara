namespace KerjaNusantara.Domain.Models.Skills;

public class SkillProfile
{
    public string CitizenId { get; set; } = string.Empty;
    public List<SkillEntry> Skills { get; set; } = new();

    public bool HasSkill(string skillName)
    {
        return Skills.Any(s => s.SkillName.Equals(skillName, StringComparison.OrdinalIgnoreCase));
    }

    public SkillEntry? GetSkill(string skillName)
    {
        return Skills.FirstOrDefault(s => s.SkillName.Equals(skillName, StringComparison.OrdinalIgnoreCase));
    }

    public void AddSkill(SkillEntry skill)
    {
        if (!HasSkill(skill.SkillName))
        {
            Skills.Add(skill);
        }
    }

    public void RemoveSkill(string skillName)
    {
        Skills.RemoveAll(s => s.SkillName.Equals(skillName, StringComparison.OrdinalIgnoreCase));
    }
}
