using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for Job entities
/// </summary>
public interface IJobRepository : IRepository<Job>
{
    /// <summary>
    /// Get all jobs by company ID
    /// </summary>
    IEnumerable<Job> GetByCompanyId(string companyId);

    /// <summary>
    /// Get all jobs with specific status
    /// </summary>
    IEnumerable<Job> GetByStatus(JobStatus status);

    /// <summary>
    /// Get all open jobs
    /// </summary>
    IEnumerable<Job> GetOpenJobs();

    /// <summary>
    /// Search jobs by location
    /// </summary>
    IEnumerable<Job> GetByLocation(string location);
}
