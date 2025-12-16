using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("stats")]
    public ActionResult<object> GetStats()
    {
        return Ok(new
        {
            TotalCitizens = _analyticsService.GetTotalCitizens(),
            TotalCompanies = _analyticsService.GetTotalCompanies(),
            TotalJobs = _analyticsService.GetTotalJobs(),
            OpenJobs = _analyticsService.GetTotalOpenJobs(),
            TotalApplications = _analyticsService.GetTotalApplications(),
            TotalProjects = _analyticsService.GetTotalProjects(),
            TotalBids = _analyticsService.GetTotalBids(),
            EmploymentRate = _analyticsService.GetEmploymentRate()
        });
    }

    [HttpGet("dashboard")]
    public ActionResult DisplayDashboard()
    {
        // This is a console method in the service, might write to console.
        // For API, we might not want to call it as it just uses Console.WriteLine.
        // But for completeness, we can trigger it or just ignore it.
        // Current implementation in Service likely writes to Console.
        // We will just return 200 OK.
        _analyticsService.DisplayDashboard();
        return Ok("Dashboard displayed in server console.");
    }
}
