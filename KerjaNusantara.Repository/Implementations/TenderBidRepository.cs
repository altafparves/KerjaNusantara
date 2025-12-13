using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for TenderBid entities
/// </summary>
public class TenderBidRepository : JsonRepository<TenderBid>, ITenderBidRepository
{
    public TenderBidRepository() : base("tenderbids.json")
    {
    }

    public IEnumerable<TenderBid> GetByProjectId(string projectId)
    {
        return Find(b => b.ProjectId == projectId);
    }

    public IEnumerable<TenderBid> GetByCompanyId(string companyId)
    {
        return Find(b => b.CompanyId == companyId);
    }

    public TenderBid? GetWinningBid(string projectId)
    {
        return _data.FirstOrDefault(b => b.ProjectId == projectId && b.IsWinner);
    }

    public bool HasBid(string companyId, string projectId)
    {
        return _data.Any(b => b.CompanyId == companyId && b.ProjectId == projectId);
    }
}
