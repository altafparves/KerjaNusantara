using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Services.Factories;

/// <summary>
/// Factory implementation for creating User objects (Factory Pattern)
/// </summary>
public class UserFactory : IUserFactory
{
    public Citizen CreateCitizen(string name, string email, string nik, string location)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(nik))
            throw new ArgumentException("NIK cannot be empty", nameof(nik));

        // Create and initialize Citizen
        var citizen = new Citizen
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            NIK = nik,
            Location = location,
            CreatedAt = DateTime.Now,
            Balance = 0,
            YearsOfExperience = 0
        };

        return citizen;
    }

    public Company CreateCompany(string name, string email, string companyName, string registrationNumber, string industry)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("Company name cannot be empty", nameof(companyName));

        // Create and initialize Company
        var company = new Company
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            CompanyName = companyName,
            CompanyRegistrationNumber = registrationNumber,
            Industry = industry,
            CreatedAt = DateTime.Now
        };

        return company;
    }

    public Government CreateGovernment(string name, string email, string agencyName, string department)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(agencyName))
            throw new ArgumentException("Agency name cannot be empty", nameof(agencyName));

        // Create and initialize Government
        var government = new Government
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            AgencyName = agencyName,
            Department = department,
            CreatedAt = DateTime.Now
        };

        return government;
    }
}
