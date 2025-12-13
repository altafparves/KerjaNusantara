using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Projects;

public class GovernmentProject : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string GovernmentId { get; set; } = string.Empty;
    public string AgencyName { get; set; } = string.Empty;
    public decimal Budget { get; set; } = 0;
    public TenderStatus Status { get; set; } = TenderStatus.Open;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? TenderClosingDate { get; set; }
    public string? AwardedToCompanyId { get; set; }
    public string? AwardedToCompanyName { get; set; }
}
