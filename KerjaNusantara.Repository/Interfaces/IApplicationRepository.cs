using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for JobApplication entities
/// </summary>
public interface IApplicationRepository : IRepository<JobApplication>
{
    /// <summary>
    /// Get all applications for a specific job
    /// </summary>
    IEnumerable<JobApplication> GetByJobId(string jobId);

    /// <summary>
    /// Get all applications by a specific citizen
    /// </summary>
    IEnumerable<JobApplication> GetByCitizenId(string citizenId);

    /// <summary>
    /// Get applications by status
    /// </summary>
    IEnumerable<JobApplication> GetByStatus(ApplicationStatus status);

    /// <summary>
    /// Check if citizen already applied to a job
    /// </summary>
    bool HasApplied(string citizenId, string jobId);
}
