using KerjaNusantara.Domain.Interfaces;

namespace KerjaNusantara.Domain.Models.Projects;

public class TenderBid : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal BidAmount { get; set; } = 0;
    public string Proposal { get; set; } = string.Empty;
    public int EstimatedDays { get; set; } = 0;
    public DateTime SubmittedDate { get; set; } = DateTime.Now;
    public bool IsWinner { get; set; } = false;
}
