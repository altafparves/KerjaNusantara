using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for GovernmentProject entities
/// </summary>
public interface IProjectRepository : IRepository<GovernmentProject>
{
    /// <summary>
    /// Get all projects by government agency
    /// </summary>
    IEnumerable<GovernmentProject> GetByGovernmentId(string governmentId);

    /// <summary>
    /// Get projects by status
    /// </summary>
    IEnumerable<GovernmentProject> GetByStatus(TenderStatus status);

    /// <summary>
    /// Get all open tenders
    /// </summary>
    IEnumerable<GovernmentProject> GetOpenTenders();
}
