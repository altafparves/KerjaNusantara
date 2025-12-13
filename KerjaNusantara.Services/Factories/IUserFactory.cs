using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Services.Factories;

/// <summary>
/// Factory interface for creating User objects
/// </summary>
public interface IUserFactory
{
    /// <summary>
    /// Create a Citizen
    /// </summary>
    Citizen CreateCitizen(string name, string email, string nik, string location);

    /// <summary>
    /// Create a Company
    /// </summary>
    Company CreateCompany(string name, string email, string companyName, string registrationNumber, string industry);

    /// <summary>
    /// Create a Government entity
    /// </summary>
    Government CreateGovernment(string name, string email, string agencyName, string department);
}
