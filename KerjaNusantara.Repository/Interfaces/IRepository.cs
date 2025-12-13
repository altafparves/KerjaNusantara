using KerjaNusantara.Domain.Interfaces;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="T">Entity type that implements IIdentifiable</typeparam>
public interface IRepository<T> where T : class, IIdentifiable
{
    /// <summary>
    /// Get entity by ID
    /// </summary>
    T? GetById(string id);

    /// <summary>
    /// Get all entities
    /// </summary>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Add new entity
    /// </summary>
    void Add(T entity);

    /// <summary>
    /// Update existing entity
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    void Delete(string id);

    /// <summary>
    /// Find entities matching a predicate
    /// </summary>
    IEnumerable<T> Find(Func<T, bool> predicate);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    bool Exists(string id);

    /// <summary>
    /// Get count of all entities
    /// </summary>
    int Count();
}
