using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Skills;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for Job management
/// </summary>
public interface IJobService
{
    // Job operations
    Job CreateJob(string companyId, string title, string description, decimal salary, string location, int minExperience, List<SkillRequirement> requirements);
    Job? GetJobById(string id);
    IEnumerable<Job> GetAllJobs();
    IEnumerable<Job> GetOpenJobs();
    IEnumerable<Job> GetJobsByCompany(string companyId);
    IEnumerable<Job> GetJobsByLocation(string location);
    void UpdateJob(Job job);
    void CloseJob(string jobId);
    
    // Application operations
    JobApplication ApplyToJob(string citizenId, string jobId, string coverLetter);
    JobApplication? GetApplicationById(string id);
    IEnumerable<JobApplication> GetApplicationsByJob(string jobId);
    IEnumerable<JobApplication> GetApplicationsByCitizen(string citizenId);
    void AcceptApplication(string applicationId);
    void RejectApplication(string applicationId);
    bool HasApplied(string citizenId, string jobId);
}
