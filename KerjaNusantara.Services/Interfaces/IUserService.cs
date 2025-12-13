using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for User management
/// </summary>
public interface IUserService
{
    // Citizen operations
    Citizen RegisterCitizen(string name, string email, string nik, string location);
    Citizen? GetCitizenById(string id);
    Citizen? GetCitizenByNIK(string nik);
    IEnumerable<Citizen> GetAllCitizens();
    void UpdateCitizen(Citizen citizen);
    
    // Company operations
    Company RegisterCompany(string name, string email, string companyName, string registrationNumber, string industry);
    Company? GetCompanyById(string id);
    IEnumerable<Company> GetAllCompanies();
    void UpdateCompany(Company company);
    
    // Government operations
    Government RegisterGovernment(string name, string email, string agencyName, string department);
    Government? GetGovernmentById(string id);
    IEnumerable<Government> GetAllGovernments();
    void UpdateGovernment(Government government);
}
