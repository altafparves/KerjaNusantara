using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Services.Factories;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// User service implementation
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository<Citizen> _citizenRepo;
    private readonly IUserRepository<Company> _companyRepo;
    private readonly IUserRepository<Government> _governmentRepo;
    private readonly IUserFactory _userFactory;

    public UserService(
        IUserRepository<Citizen> citizenRepo,
        IUserRepository<Company> companyRepo,
        IUserRepository<Government> governmentRepo,
        IUserFactory userFactory)
    {
        _citizenRepo = citizenRepo;
        _companyRepo = companyRepo;
        _governmentRepo = governmentRepo;
        _userFactory = userFactory;
    }

    // Citizen operations
    public Citizen RegisterCitizen(string name, string email, string nik, string location)
    {
        // Check if NIK already exists
        if (_citizenRepo.GetCitizenByNIK(nik) != null)
            throw new InvalidOperationException($"Citizen with NIK {nik} already exists");

        // Check if email already exists
        if (_citizenRepo.GetByEmail(email) != null)
            throw new InvalidOperationException($"Email {email} is already registered");

        // Use factory to create citizen
        var citizen = _userFactory.CreateCitizen(name, email, nik, location);
        
        _citizenRepo.Add(citizen);
        return citizen;
    }

    public Citizen? GetCitizenById(string id) => _citizenRepo.GetById(id);

    public Citizen? GetCitizenByNIK(string nik) => _citizenRepo.GetCitizenByNIK(nik);

    public IEnumerable<Citizen> GetAllCitizens() => _citizenRepo.GetAll();

    public void UpdateCitizen(Citizen citizen) => _citizenRepo.Update(citizen);

    // Company operations
    public Company RegisterCompany(string name, string email, string companyName, string registrationNumber, string industry)
    {
        // Check if email already exists
        if (_companyRepo.GetByEmail(email) != null)
            throw new InvalidOperationException($"Email {email} is already registered");

        // Use factory to create company
        var company = _userFactory.CreateCompany(name, email, companyName, registrationNumber, industry);
        
        _companyRepo.Add(company);
        return company;
    }

    public Company? GetCompanyById(string id) => _companyRepo.GetById(id);

    public IEnumerable<Company> GetAllCompanies() => _companyRepo.GetAll();

    public void UpdateCompany(Company company) => _companyRepo.Update(company);

    // Government operations
    public Government RegisterGovernment(string name, string email, string agencyName, string department)
    {
        // Check if email already exists
        if (_governmentRepo.GetByEmail(email) != null)
            throw new InvalidOperationException($"Email {email} is already registered");

        // Use factory to create government
        var government = _userFactory.CreateGovernment(name, email, agencyName, department);
        
        _governmentRepo.Add(government);
        return government;
    }

    public Government? GetGovernmentById(string id) => _governmentRepo.GetById(id);

    public IEnumerable<Government> GetAllGovernments() => _governmentRepo.GetAll();

    public void UpdateGovernment(Government government) => _governmentRepo.Update(government);
}
