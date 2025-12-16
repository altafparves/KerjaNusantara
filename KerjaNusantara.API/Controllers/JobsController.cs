using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.API.DTOs;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public ActionResult<Job> CreateJob([FromBody] CreateJobRequest request)
    {
        try
        {
            var job = _jobService.CreateJob(request.CompanyId, request.Title, request.Description, request.Salary, request.Location, request.MinExperience, request.Requirements);
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Job> GetJob(string id)
    {
        var job = _jobService.GetJobById(id);
        if (job == null) return NotFound();
        return Ok(job);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Job>> GetAllJobs([FromQuery] string? companyId = null, [FromQuery] string? location = null, [FromQuery] bool openOnly = false)
    {
        if (companyId != null)
            return Ok(_jobService.GetJobsByCompany(companyId));
        if (location != null)
            return Ok(_jobService.GetJobsByLocation(location));
        if (openOnly)
            return Ok(_jobService.GetOpenJobs());
            
        return Ok(_jobService.GetAllJobs());
    }

    [HttpPost("applications")]
    public ActionResult<JobApplication> ApplyToJob([FromBody] ApplyToJobRequest request)
    {
        try
        {
            var application = _jobService.ApplyToJob(request.CitizenId, request.JobId, request.CoverLetter);
            return Ok(application);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("applications/{id}")]
    public ActionResult<JobApplication> GetApplication(string id)
    {
        var app = _jobService.GetApplicationById(id);
        if (app == null) return NotFound();
        return Ok(app);
    }

    [HttpGet("jobs/{jobId}/applications")]
    public ActionResult<IEnumerable<JobApplication>> GetApplicationsByJob(string jobId)
    {
        return Ok(_jobService.GetApplicationsByJob(jobId));
    }
    
    [HttpPost("applications/{id}/accept")]
    public ActionResult AcceptApplication(string id)
    {
        _jobService.AcceptApplication(id);
        return Ok();
    }

    [HttpPost("applications/{id}/reject")]
    public ActionResult RejectApplication(string id)
    {
        _jobService.RejectApplication(id);
        return Ok();
    }
}
