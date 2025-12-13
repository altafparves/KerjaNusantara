using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for User entities
/// </summary>
public interface IUserRepository<T> : IRepository<T> where T : User
{
    /// <summary>
    /// Find user by email
    /// </summary>
    T? GetByEmail(string email);

    /// <summary>
    /// Find citizen by NIK
    /// </summary>
    Citizen? GetCitizenByNIK(string nik);

    /// <summary>
    /// Get all users of specific type
    /// </summary>
    IEnumerable<T> GetAllOfType();
}
