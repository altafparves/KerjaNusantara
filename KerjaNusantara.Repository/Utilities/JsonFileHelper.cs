using System.Text.Json;
using System.Text.Json.Serialization;

namespace KerjaNusantara.Repository.Utilities;

/// <summary>
/// Helper class for JSON file operations
/// </summary>
public static class JsonFileHelper
{
    private static readonly string DataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    /// <summary>
    /// Ensure data directory exists
    /// </summary>
    public static void EnsureDataDirectoryExists()
    {
        if (!Directory.Exists(DataDirectory))
        {
            Directory.CreateDirectory(DataDirectory);
        }
    }

    /// <summary>
    /// Get full file path for a data file
    /// </summary>
    public static string GetFilePath(string fileName)
    {
        EnsureDataDirectoryExists();
        return Path.Combine(DataDirectory, fileName);
    }

    /// <summary>
    /// Save data to JSON file
    /// </summary>
    public static void SaveToFile<T>(string fileName, List<T> data)
    {
        try
        {
            var filePath = GetFilePath(fileName);
            var json = JsonSerializer.Serialize(data, Options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to {fileName}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Load data from JSON file
    /// </summary>
    public static List<T> LoadFromFile<T>(string fileName)
    {
        try
        {
            var filePath = GetFilePath(fileName);
            
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);
            
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from {fileName}: {ex.Message}");
            return new List<T>();
        }
    }

    /// <summary>
    /// Check if file exists
    /// </summary>
    public static bool FileExists(string fileName)
    {
        var filePath = GetFilePath(fileName);
        return File.Exists(filePath);
    }

    /// <summary>
    /// Delete file
    /// </summary>
    public static void DeleteFile(string fileName)
    {
        var filePath = GetFilePath(fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
