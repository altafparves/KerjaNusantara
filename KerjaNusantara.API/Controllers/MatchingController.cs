using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Matching;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchingController : ControllerBase
{
    private readonly IMatchingService _matchingService;

    public MatchingController(IMatchingService matchingService)
    {
        _matchingService = matchingService;
    }

    [HttpGet("recommendations/{citizenId}")]
    public ActionResult<List<MatchResult>> GetRecommendations(string citizenId, [FromQuery] int topN = 10)
    {
        return Ok(_matchingService.GetRecommendations(citizenId, topN));
    }

    [HttpGet("match/{citizenId}/{jobId}")]
    public ActionResult<MatchResult> CalculateMatch(string citizenId, string jobId)
    {
        return Ok(_matchingService.CalculateMatch(citizenId, jobId));
    }

    [HttpGet("all/{citizenId}")]
    public ActionResult<List<MatchResult>> GetAllMatches(string citizenId)
    {
        return Ok(_matchingService.GetAllMatches(citizenId));
    }
}
