using KerjaNusantara.Domain.Models.Skills;

namespace KerjaNusantara.API.DTOs;

public record CreateJobRequest(string CompanyId, string Title, string Description, decimal Salary, string Location, int MinExperience, List<SkillRequirement> Requirements);

public record ApplyToJobRequest(string CitizenId, string JobId, string CoverLetter);
