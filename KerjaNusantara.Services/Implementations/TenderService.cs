using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// Tender service implementation
/// </summary>
public class TenderService : ITenderService
{
    private readonly IProjectRepository _projectRepo;
    private readonly ITenderBidRepository _bidRepo;
    private readonly IUserRepository<Domain.Models.Users.Government> _governmentRepo;
    private readonly IUserRepository<Domain.Models.Users.Company> _companyRepo;

    public TenderService(
        IProjectRepository projectRepo,
        ITenderBidRepository bidRepo,
        IUserRepository<Domain.Models.Users.Government> governmentRepo,
        IUserRepository<Domain.Models.Users.Company> companyRepo)
    {
        _projectRepo = projectRepo;
        _bidRepo = bidRepo;
        _governmentRepo = governmentRepo;
        _companyRepo = companyRepo;
    }

    // Project operations
    public GovernmentProject CreateProject(string governmentId, string title, string description, decimal budget, DateTime? closingDate)
    {
        var government = _governmentRepo.GetById(governmentId);
        if (government == null)
            throw new InvalidOperationException("Government entity not found");

        var project = new GovernmentProject
        {
            Id = Guid.NewGuid().ToString(),
            GovernmentId = governmentId,
            AgencyName = government.AgencyName,
            Title = title,
            Description = description,
            Budget = budget,
            Status = TenderStatus.Open,
            CreatedDate = DateTime.Now,
            TenderClosingDate = closingDate
        };

        _projectRepo.Add(project);
        return project;
    }

    public GovernmentProject? GetProjectById(string id) => _projectRepo.GetById(id);

    public IEnumerable<GovernmentProject> GetAllProjects() => _projectRepo.GetAll();

    public IEnumerable<GovernmentProject> GetOpenTenders() => _projectRepo.GetOpenTenders();

    public IEnumerable<GovernmentProject> GetProjectsByGovernment(string governmentId) => _projectRepo.GetByGovernmentId(governmentId);

    public void UpdateProject(GovernmentProject project) => _projectRepo.Update(project);

    public void CloseTender(string projectId)
    {
        var project = _projectRepo.GetById(projectId);
        if (project != null)
        {
            project.Status = TenderStatus.Closed;
            _projectRepo.Update(project);
        }
    }

    // Bid operations
    public TenderBid SubmitBid(string companyId, string projectId, decimal bidAmount, string proposal, int estimatedDays)
    {
        var company = _companyRepo.GetById(companyId);
        if (company == null)
            throw new InvalidOperationException("Company not found");

        var project = _projectRepo.GetById(projectId);
        if (project == null)
            throw new InvalidOperationException("Project not found");

        if (project.Status != TenderStatus.Open)
            throw new InvalidOperationException("Tender is not open for bidding");

        if (_bidRepo.HasBid(companyId, projectId))
            throw new InvalidOperationException("Company has already submitted a bid for this project");

        var bid = new TenderBid
        {
            Id = Guid.NewGuid().ToString(),
            ProjectId = projectId,
            CompanyId = companyId,
            CompanyName = company.CompanyName,
            BidAmount = bidAmount,
            Proposal = proposal,
            EstimatedDays = estimatedDays,
            SubmittedDate = DateTime.Now,
            IsWinner = false
        };

        _bidRepo.Add(bid);
        return bid;
    }

    public TenderBid? GetBidById(string id) => _bidRepo.GetById(id);

    public IEnumerable<TenderBid> GetBidsByProject(string projectId) => _bidRepo.GetByProjectId(projectId);

    public IEnumerable<TenderBid> GetBidsByCompany(string companyId) => _bidRepo.GetByCompanyId(companyId);

    public void AwardTender(string projectId, string winningBidId)
    {
        var project = _projectRepo.GetById(projectId);
        if (project == null)
            throw new InvalidOperationException("Project not found");

        var winningBid = _bidRepo.GetById(winningBidId);
        if (winningBid == null || winningBid.ProjectId != projectId)
            throw new InvalidOperationException("Invalid bid for this project");

        // Mark bid as winner
        winningBid.IsWinner = true;
        _bidRepo.Update(winningBid);

        // Update project
        project.Status = TenderStatus.Awarded;
        project.AwardedToCompanyId = winningBid.CompanyId;
        project.AwardedToCompanyName = winningBid.CompanyName;
        _projectRepo.Update(project);
    }

    public bool HasBid(string companyId, string projectId) => _bidRepo.HasBid(companyId, projectId);
}
