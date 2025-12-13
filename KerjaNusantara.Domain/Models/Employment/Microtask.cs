using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Employment;

public class Microtask : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PostedBy { get; set; } = string.Empty; // Government or Company ID
    public decimal Payment { get; set; } = 0;
    public JobStatus Status { get; set; } = JobStatus.Open;
    public string? AssignedTo { get; set; } // Citizen ID
    public DateTime PostedDate { get; set; } = DateTime.Now;
    public DateTime? CompletedDate { get; set; }
    public int EstimatedHours { get; set; } = 0;
}
