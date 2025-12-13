using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for JobApplication entities
/// </summary>
public class ApplicationRepository : JsonRepository<JobApplication>, IApplicationRepository
{
    public ApplicationRepository() : base("applications.json")
    {
    }

    public IEnumerable<JobApplication> GetByJobId(string jobId)
    {
        return Find(a => a.JobId == jobId);
    }

    public IEnumerable<JobApplication> GetByCitizenId(string citizenId)
    {
        return Find(a => a.CitizenId == citizenId);
    }

    public IEnumerable<JobApplication> GetByStatus(ApplicationStatus status)
    {
        return Find(a => a.Status == status);
    }

    public bool HasApplied(string citizenId, string jobId)
    {
        return _data.Any(a => a.CitizenId == citizenId && a.JobId == jobId);
    }
}
