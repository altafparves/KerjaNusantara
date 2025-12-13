using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for Job entities
/// </summary>
public class JobRepository : JsonRepository<Job>, IJobRepository
{
    public JobRepository() : base("jobs.json")
    {
    }

    public IEnumerable<Job> GetByCompanyId(string companyId)
    {
        return Find(j => j.CompanyId == companyId);
    }

    public IEnumerable<Job> GetByStatus(JobStatus status)
    {
        return Find(j => j.Status == status);
    }

    public IEnumerable<Job> GetOpenJobs()
    {
        return GetByStatus(JobStatus.Open);
    }

    public IEnumerable<Job> GetByLocation(string location)
    {
        return Find(j => j.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
    }
}
