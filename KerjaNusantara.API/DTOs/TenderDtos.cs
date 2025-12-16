namespace KerjaNusantara.API.DTOs;

public record CreateProjectRequest(string GovernmentId, string Title, string Description, decimal Budget, DateTime? ClosingDate);

public record SubmitBidRequest(string CompanyId, string ProjectId, decimal BidAmount, string Proposal, int EstimatedDays);
