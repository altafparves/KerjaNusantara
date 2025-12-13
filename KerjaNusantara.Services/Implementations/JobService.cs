using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Models.Skills;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// Job service implementation
/// </summary>
public class JobService : IJobService
{
    private readonly IJobRepository _jobRepo;
    private readonly IApplicationRepository _applicationRepo;
    private readonly IUserRepository<Domain.Models.Users.Citizen> _citizenRepo;
    private readonly IUserRepository<Domain.Models.Users.Company> _companyRepo;
    private readonly IMatchingService _matchingService;

    public JobService(
        IJobRepository jobRepo,
        IApplicationRepository applicationRepo,
        IUserRepository<Domain.Models.Users.Citizen> citizenRepo,
        IUserRepository<Domain.Models.Users.Company> companyRepo,
        IMatchingService matchingService)
    {
        _jobRepo = jobRepo;
        _applicationRepo = applicationRepo;
        _citizenRepo = citizenRepo;
        _companyRepo = companyRepo;
        _matchingService = matchingService;
    }

    // Job operations
    public Job CreateJob(string companyId, string title, string description, decimal salary, string location, int minExperience, List<SkillRequirement> requirements)
    {
        var company = _companyRepo.GetById(companyId);
        if (company == null)
            throw new InvalidOperationException("Company not found");

        var job = new Job
        {
            Id = Guid.NewGuid().ToString(),
            CompanyId = companyId,
            CompanyName = company.CompanyName,
            Title = title,
            Description = description,
            Salary = salary,
            Location = location,
            MinExperience = minExperience,
            Requirements = requirements,
            Status = JobStatus.Open,
            PostedDate = DateTime.Now
        };

        _jobRepo.Add(job);
        return job;
    }

    public Job? GetJobById(string id) => _jobRepo.GetById(id);

    public IEnumerable<Job> GetAllJobs() => _jobRepo.GetAll();

    public IEnumerable<Job> GetOpenJobs() => _jobRepo.GetOpenJobs();

    public IEnumerable<Job> GetJobsByCompany(string companyId) => _jobRepo.GetByCompanyId(companyId);

    public IEnumerable<Job> GetJobsByLocation(string location) => _jobRepo.GetByLocation(location);

    public void UpdateJob(Job job) => _jobRepo.Update(job);

    public void CloseJob(string jobId)
    {
        var job = _jobRepo.GetById(jobId);
        if (job != null)
        {
            job.Status = JobStatus.Closed;
            job.ClosedDate = DateTime.Now;
            _jobRepo.Update(job);
        }
    }

    // Application operations
    public JobApplication ApplyToJob(string citizenId, string jobId, string coverLetter)
    {
        var citizen = _citizenRepo.GetById(citizenId);
        if (citizen == null)
            throw new InvalidOperationException("Citizen not found");

        var job = _jobRepo.GetById(jobId);
        if (job == null)
            throw new InvalidOperationException("Job not found");

        if (job.Status != JobStatus.Open)
            throw new InvalidOperationException("Job is not open for applications");

        if (_applicationRepo.HasApplied(citizenId, jobId))
            throw new InvalidOperationException("You have already applied to this job");

        // Calculate match score
        var matchResult = _matchingService.CalculateMatch(citizenId, jobId);

        var application = new JobApplication
        {
            Id = Guid.NewGuid().ToString(),
            JobId = jobId,
            CitizenId = citizenId,
            CitizenName = citizen.Name,
            Status = ApplicationStatus.Pending,
            AppliedDate = DateTime.Now,
            MatchScore = matchResult.MatchScore,
            CoverLetter = coverLetter
        };

        _applicationRepo.Add(application);
        return application;
    }

    public JobApplication? GetApplicationById(string id) => _applicationRepo.GetById(id);

    public IEnumerable<JobApplication> GetApplicationsByJob(string jobId) => _applicationRepo.GetByJobId(jobId);

    public IEnumerable<JobApplication> GetApplicationsByCitizen(string citizenId) => _applicationRepo.GetByCitizenId(citizenId);

    public void AcceptApplication(string applicationId)
    {
        var application = _applicationRepo.GetById(applicationId);
        if (application != null)
        {
            application.Status = ApplicationStatus.Accepted;
            application.ReviewedDate = DateTime.Now;
            _applicationRepo.Update(application);
        }
    }

    public void RejectApplication(string applicationId)
    {
        var application = _applicationRepo.GetById(applicationId);
        if (application != null)
        {
            application.Status = ApplicationStatus.Rejected;
            application.ReviewedDate = DateTime.Now;
            _applicationRepo.Update(application);
        }
    }

    public bool HasApplied(string citizenId, string jobId) => _applicationRepo.HasApplied(citizenId, jobId);
}
