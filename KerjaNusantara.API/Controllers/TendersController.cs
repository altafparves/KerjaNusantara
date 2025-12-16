using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Projects;
using KerjaNusantara.API.DTOs;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TendersController : ControllerBase
{
    private readonly ITenderService _tenderService;

    public TendersController(ITenderService tenderService)
    {
        _tenderService = tenderService;
    }

    [HttpPost("projects")]
    public ActionResult<GovernmentProject> CreateProject([FromBody] CreateProjectRequest request)
    {
        try
        {
            var project = _tenderService.CreateProject(request.GovernmentId, request.Title, request.Description, request.Budget, request.ClosingDate);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("projects/{id}")]
    public ActionResult<GovernmentProject> GetProject(string id)
    {
        var project = _tenderService.GetProjectById(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpGet("projects")]
    public ActionResult<IEnumerable<GovernmentProject>> GetAllProjects([FromQuery] string? governmentId = null, [FromQuery] bool openOnly = false)
    {
        if (governmentId != null)
            return Ok(_tenderService.GetProjectsByGovernment(governmentId));
        if (openOnly)
            return Ok(_tenderService.GetOpenTenders());
            
        return Ok(_tenderService.GetAllProjects());
    }

    [HttpPost("bids")]
    public ActionResult<TenderBid> SubmitBid([FromBody] SubmitBidRequest request)
    {
        try
        {
            var bid = _tenderService.SubmitBid(request.CompanyId, request.ProjectId, request.BidAmount, request.Proposal, request.EstimatedDays);
            return Ok(bid);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("bids/{id}")]
    public ActionResult<TenderBid> GetBid(string id)
    {
        var bid = _tenderService.GetBidById(id);
        if (bid == null) return NotFound();
        return Ok(bid);
    }

    [HttpGet("projects/{projectId}/bids")]
    public ActionResult<IEnumerable<TenderBid>> GetBidsByProject(string projectId)
    {
        return Ok(_tenderService.GetBidsByProject(projectId));
    }

    [HttpPost("projects/{projectId}/award")]
    public ActionResult AwardTender(string projectId, [FromQuery] string winningBidId)
    {
        try
        {
            _tenderService.AwardTender(projectId, winningBidId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
