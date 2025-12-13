using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for Government entities
/// </summary>
public class GovernmentRepository : JsonRepository<Government>, IUserRepository<Government>
{
    public GovernmentRepository() : base("government.json")
    {
    }

    public Government? GetByEmail(string email)
    {
        return _data.FirstOrDefault(g => g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public Citizen? GetCitizenByNIK(string nik)
    {
        // Not applicable for government
        return null;
    }

    public IEnumerable<Government> GetAllOfType()
    {
        return GetAll();
    }
}
