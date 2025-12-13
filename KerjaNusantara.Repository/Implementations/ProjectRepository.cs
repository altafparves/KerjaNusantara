using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for GovernmentProject entities
/// </summary>
public class ProjectRepository : JsonRepository<GovernmentProject>, IProjectRepository
{
    public ProjectRepository() : base("projects.json")
    {
    }

    public IEnumerable<GovernmentProject> GetByGovernmentId(string governmentId)
    {
        return Find(p => p.GovernmentId == governmentId);
    }

    public IEnumerable<GovernmentProject> GetByStatus(TenderStatus status)
    {
        return Find(p => p.Status == status);
    }

    public IEnumerable<GovernmentProject> GetOpenTenders()
    {
        return GetByStatus(TenderStatus.Open);
    }
}
