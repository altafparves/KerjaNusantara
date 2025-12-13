using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Domain.Models.Skills;

namespace KerjaNusantara.Domain.Models.Matching;

public class MatchResult
{
    public Job Job { get; set; } = null!;
    public Citizen Citizen { get; set; } = null!;
    public int MatchScore { get; set; } = 0; // 0-100
    public List<SkillGap> SkillGaps { get; set; } = new();
    public string Recommendation { get; set; } = string.Empty;

    public string GetMatchLevel()
    {
        return MatchScore switch
        {
            >= 70 => "Highly Recommended",
            >= 50 => "Good Match",
            >= 30 => "Potential Match (Training Needed)",
            _ => "Not Recommended"
        };
    }
}
