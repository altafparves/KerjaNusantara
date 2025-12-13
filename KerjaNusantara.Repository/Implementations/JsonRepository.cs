using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Repository.Utilities;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Generic repository implementation using JSON file storage
/// </summary>
public class JsonRepository<T> : IRepository<T> where T : class, IIdentifiable
{
    protected readonly List<T> _data;
    protected readonly string _fileName;

    public JsonRepository(string fileName)
    {
        _fileName = fileName;
        _data = JsonFileHelper.LoadFromFile<T>(_fileName);
    }

    public virtual T? GetById(string id)
    {
        return _data.FirstOrDefault(x => x.Id == id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _data.AsReadOnly();
    }

    public virtual void Add(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (string.IsNullOrWhiteSpace(entity.Id))
            entity.Id = Guid.NewGuid().ToString();

        if (Exists(entity.Id))
            throw new InvalidOperationException($"Entity with ID {entity.Id} already exists");

        _data.Add(entity);
        SaveChanges();
    }

    public virtual void Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var existing = GetById(entity.Id);
        if (existing == null)
            throw new InvalidOperationException($"Entity with ID {entity.Id} not found");

        _data.Remove(existing);
        _data.Add(entity);
        SaveChanges();
    }

    public virtual void Delete(string id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _data.Remove(entity);
            SaveChanges();
        }
    }

    public virtual IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return _data.Where(predicate).ToList();
    }

    public virtual bool Exists(string id)
    {
        return _data.Any(x => x.Id == id);
    }

    public virtual int Count()
    {
        return _data.Count;
    }

    protected virtual void SaveChanges()
    {
        JsonFileHelper.SaveToFile(_fileName, _data);
    }
}
