using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Domain.Models.Skills;

namespace KerjaNusantara.Domain.Models.Employment;

public class Job : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Salary { get; set; } = 0;
    public string Location { get; set; } = string.Empty;
    public JobStatus Status { get; set; } = JobStatus.Open;
    public List<SkillRequirement> Requirements { get; set; } = new();
    public int MinExperience { get; set; } = 0; // Minimum years of experience
    public DateTime PostedDate { get; set; } = DateTime.Now;
    public DateTime? ClosedDate { get; set; }
}
