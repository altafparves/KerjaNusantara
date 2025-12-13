using KerjaNusantara.Domain.Models.Matching;
using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Services.Matching;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// Matching service implementation using Strategy Pattern
/// </summary>
public class MatchingService : IMatchingService
{
    private readonly IUserRepository<Citizen> _citizenRepo;
    private readonly IJobRepository _jobRepo;
    private IMatchingStrategy _strategy;

    public MatchingService(
        IUserRepository<Citizen> citizenRepo,
        IJobRepository jobRepo,
        IMatchingStrategy strategy)
    {
        _citizenRepo = citizenRepo;
        _jobRepo = jobRepo;
        _strategy = strategy;
    }

    /// <summary>
    /// Set matching strategy (allows runtime strategy switching)
    /// </summary>
    public void SetStrategy(IMatchingStrategy strategy)
    {
        _strategy = strategy;
    }

    public List<MatchResult> GetRecommendations(string citizenId, int topN = 10)
    {
        var citizen = _citizenRepo.GetById(citizenId);
        if (citizen == null)
            throw new InvalidOperationException("Citizen not found");

        var openJobs = _jobRepo.GetOpenJobs().ToList();
        
        return _strategy.FindBestMatches(citizen, openJobs, topN);
    }

    public MatchResult CalculateMatch(string citizenId, string jobId)
    {
        var citizen = _citizenRepo.GetById(citizenId);
        if (citizen == null)
            throw new InvalidOperationException("Citizen not found");

        var job = _jobRepo.GetById(jobId);
        if (job == null)
            throw new InvalidOperationException("Job not found");

        return _strategy.CalculateMatch(citizen, job);
    }

    public List<MatchResult> GetAllMatches(string citizenId)
    {
        var citizen = _citizenRepo.GetById(citizenId);
        if (citizen == null)
            throw new InvalidOperationException("Citizen not found");

        var openJobs = _jobRepo.GetOpenJobs().ToList();
        
        return _strategy.FindBestMatches(citizen, openJobs, openJobs.Count);
    }
}
