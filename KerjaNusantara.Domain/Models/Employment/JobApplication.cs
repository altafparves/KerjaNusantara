using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Employment;

public class JobApplication : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JobId { get; set; } = string.Empty;
    public string CitizenId { get; set; } = string.Empty;
    public string CitizenName { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public DateTime AppliedDate { get; set; } = DateTime.Now;
    public DateTime? ReviewedDate { get; set; }
    public int MatchScore { get; set; } = 0; // 0-100
    public string CoverLetter { get; set; } = string.Empty;
}
