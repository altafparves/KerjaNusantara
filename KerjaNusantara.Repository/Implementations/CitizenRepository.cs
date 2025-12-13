using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for Citizen entities
/// </summary>
public class CitizenRepository : JsonRepository<Citizen>, IUserRepository<Citizen>
{
    public CitizenRepository() : base("citizens.json")
    {
    }

    public Citizen? GetByEmail(string email)
    {
        return _data.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public Citizen? GetCitizenByNIK(string nik)
    {
        return _data.FirstOrDefault(c => c.NIK.Equals(nik, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Citizen> GetAllOfType()
    {
        return GetAll();
    }
}
