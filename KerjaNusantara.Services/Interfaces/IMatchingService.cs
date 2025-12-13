using KerjaNusantara.Domain.Models.Matching;
using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for AI-based job matching
/// </summary>
public interface IMatchingService
{
    /// <summary>
    /// Get job recommendations for a citizen
    /// </summary>
    List<MatchResult> GetRecommendations(string citizenId, int topN = 10);
    
    /// <summary>
    /// Calculate match score between citizen and specific job
    /// </summary>
    MatchResult CalculateMatch(string citizenId, string jobId);
    
    /// <summary>
    /// Get all matches for a citizen (all open jobs)
    /// </summary>
    List<MatchResult> GetAllMatches(string citizenId);
}
