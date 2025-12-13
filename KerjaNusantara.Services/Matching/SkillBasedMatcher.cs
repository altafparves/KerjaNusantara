using KerjaNusantara.Domain.Models.Matching;
using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Services.Matching;

/// <summary>
/// Simple skill-based matching strategy (Strategy Pattern Implementation)
/// Matching formula: 70% skill match + 30% experience match
/// </summary>
public class SkillBasedMatcher : IMatchingStrategy
{
    public MatchResult CalculateMatch(Citizen citizen, Job job)
    {
        // Calculate skill match (70% weight)
        int skillScore = CalculateSkillMatch(citizen, job);
        
        // Calculate experience match (30% weight)
        int experienceScore = CalculateExperienceMatch(citizen, job);
        
        // Combined score
        int totalScore = (int)(skillScore * 0.7 + experienceScore * 0.3);
        
        // Find skill gaps
        var skillGaps = FindSkillGaps(citizen, job);
        
        var result = new MatchResult
        {
            Citizen = citizen,
            Job = job,
            MatchScore = totalScore,
            SkillGaps = skillGaps,
            Recommendation = GenerateRecommendation(totalScore, skillGaps)
        };
        
        return result;
    }

    public List<MatchResult> FindBestMatches(Citizen citizen, List<Job> jobs, int topN = 10)
    {
        var matches = new List<MatchResult>();
        
        foreach (var job in jobs)
        {
            if (job.Status == JobStatus.Open)
            {
                var match = CalculateMatch(citizen, job);
                matches.Add(match);
            }
        }
        
        // Sort by match score descending and take top N
        return matches
            .OrderByDescending(m => m.MatchScore)
            .Take(topN)
            .ToList();
    }

    private int CalculateSkillMatch(Citizen citizen, Job job)
    {
        if (job.Requirements.Count == 0)
            return 100; // No requirements = perfect match
        
        int matchedSkills = 0;
        int totalRequiredSkills = job.Requirements.Count(r => r.IsRequired);
        
        if (totalRequiredSkills == 0)
            return 100;
        
        foreach (var requirement in job.Requirements.Where(r => r.IsRequired))
        {
            var citizenSkill = citizen.SkillProfile.GetSkill(requirement.SkillName);
            
            if (citizenSkill != null && citizenSkill.Level >= requirement.MinimumLevel)
            {
                matchedSkills++;
            }
        }
        
        return (matchedSkills * 100) / totalRequiredSkills;
    }

    private int CalculateExperienceMatch(Citizen citizen, Job job)
    {
        if (job.MinExperience == 0)
            return 100; // No experience requirement
        
        if (citizen.YearsOfExperience >= job.MinExperience)
            return 100; // Meets or exceeds requirement
        
        // Partial credit for some experience
        return (citizen.YearsOfExperience * 100) / job.MinExperience;
    }

    private List<SkillGap> FindSkillGaps(Citizen citizen, Job job)
    {
        var gaps = new List<SkillGap>();
        
        foreach (var requirement in job.Requirements)
        {
            var citizenSkill = citizen.SkillProfile.GetSkill(requirement.SkillName);
            
            var gap = new SkillGap
            {
                RequiredSkillName = requirement.SkillName,
                RequiredLevel = requirement.MinimumLevel,
                CurrentLevel = citizenSkill?.Level,
                TrainingRecommendation = GenerateTrainingRecommendation(requirement.SkillName, requirement.MinimumLevel, citizenSkill?.Level)
            };
            
            gaps.Add(gap);
        }
        
        return gaps;
    }

    private string GenerateTrainingRecommendation(string skillName, SkillLevel requiredLevel, SkillLevel? currentLevel)
    {
        if (!currentLevel.HasValue)
            return $"Learn {skillName} basics to reach {requiredLevel} level";
        
        if (currentLevel < requiredLevel)
            return $"Improve {skillName} from {currentLevel} to {requiredLevel}";
        
        return $"{skillName} skill meets requirements";
    }

    private string GenerateRecommendation(int matchScore, List<SkillGap> skillGaps)
    {
        var missingSkills = skillGaps.Count(g => !g.HasSkill);
        var insufficientSkills = skillGaps.Count(g => g.HasSkill && g.CurrentLevel < g.RequiredLevel);
        
        if (matchScore >= 70)
            return "Highly recommended! You meet most requirements.";
        
        if (matchScore >= 50)
            return $"Good match. Consider improving {insufficientSkills} skill(s).";
        
        if (matchScore >= 30)
            return $"Potential match. You need to learn {missingSkills} skill(s) and improve {insufficientSkills} skill(s).";
        
        return "Not recommended. Significant skill gaps exist.";
    }
}
