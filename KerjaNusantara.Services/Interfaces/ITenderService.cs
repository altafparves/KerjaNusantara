using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for Government tender management
/// </summary>
public interface ITenderService
{
    // Project operations
    GovernmentProject CreateProject(string governmentId, string title, string description, decimal budget, DateTime? closingDate);
    GovernmentProject? GetProjectById(string id);
    IEnumerable<GovernmentProject> GetAllProjects();
    IEnumerable<GovernmentProject> GetOpenTenders();
    IEnumerable<GovernmentProject> GetProjectsByGovernment(string governmentId);
    void UpdateProject(GovernmentProject project);
    void CloseTender(string projectId);
    
    // Bid operations
    TenderBid SubmitBid(string companyId, string projectId, decimal bidAmount, string proposal, int estimatedDays);
    TenderBid? GetBidById(string id);
    IEnumerable<TenderBid> GetBidsByProject(string projectId);
    IEnumerable<TenderBid> GetBidsByCompany(string companyId);
    void AwardTender(string projectId, string winningBidId);
    bool HasBid(string companyId, string projectId);
}
