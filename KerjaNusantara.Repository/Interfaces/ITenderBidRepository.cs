using KerjaNusantara.Domain.Models.Projects;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for TenderBid entities
/// </summary>
public interface ITenderBidRepository : IRepository<TenderBid>
{
    /// <summary>
    /// Get all bids for a specific project
    /// </summary>
    IEnumerable<TenderBid> GetByProjectId(string projectId);

    /// <summary>
    /// Get all bids by a specific company
    /// </summary>
    IEnumerable<TenderBid> GetByCompanyId(string companyId);

    /// <summary>
    /// Get winning bid for a project
    /// </summary>
    TenderBid? GetWinningBid(string projectId);

    /// <summary>
    /// Check if company already bid on project
    /// </summary>
    bool HasBid(string companyId, string projectId);
}
