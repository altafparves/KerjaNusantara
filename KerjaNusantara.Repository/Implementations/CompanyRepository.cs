using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for Company entities
/// </summary>
public class CompanyRepository : JsonRepository<Company>, IUserRepository<Company>
{
    public CompanyRepository() : base("companies.json")
    {
    }

    public Company? GetByEmail(string email)
    {
        return _data.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public Citizen? GetCitizenByNIK(string nik)
    {
        // Not applicable for companies
        return null;
    }

    public IEnumerable<Company> GetAllOfType()
    {
        return GetAll();
    }
}
