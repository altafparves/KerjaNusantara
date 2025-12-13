namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for Analytics and reporting
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Get total number of registered citizens
    /// </summary>
    int GetTotalCitizens();
    
    /// <summary>
    /// Get total number of registered companies
    /// </summary>
    int GetTotalCompanies();
    
    /// <summary>
    /// Get total number of job postings
    /// </summary>
    int GetTotalJobs();
    
    /// <summary>
    /// Get total number of open jobs
    /// </summary>
    int GetTotalOpenJobs();
    
    /// <summary>
    /// Get total number of applications
    /// </summary>
    int GetTotalApplications();
    
    /// <summary>
    /// Get total number of government projects
    /// </summary>
    int GetTotalProjects();
    
    /// <summary>
    /// Get total number of tender bids
    /// </summary>
    int GetTotalBids();
    
    /// <summary>
    /// Get employment rate (accepted applications / total applications)
    /// </summary>
    double GetEmploymentRate();
    
    /// <summary>
    /// Display analytics dashboard
    /// </summary>
    void DisplayDashboard();
}
