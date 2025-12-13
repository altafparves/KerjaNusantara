using KerjaNusantara.Domain.Models.Matching;
using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Services.Matching;

/// <summary>
/// Strategy interface for job matching algorithms (Strategy Pattern)
/// </summary>
public interface IMatchingStrategy
{
    /// <summary>
    /// Calculate match between a citizen and a job
    /// </summary>
    MatchResult CalculateMatch(Citizen citizen, Job job);

    /// <summary>
    /// Find best job matches for a citizen
    /// </summary>
    List<MatchResult> FindBestMatches(Citizen citizen, List<Job> jobs, int topN = 10);
}
