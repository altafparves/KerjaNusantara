using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.API.DTOs;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // Citizens
    [HttpPost("citizens")]
    public ActionResult<Citizen> RegisterCitizen([FromBody] RegisterCitizenRequest request)
    {
        try
        {
            var citizen = _userService.RegisterCitizen(request.Name, request.Email, request.NIK, request.Location);
            return CreatedAtAction(nameof(GetCitizen), new { id = citizen.Id }, citizen);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("citizens/{id}")]
    public ActionResult<Citizen> GetCitizen(string id)
    {
        var citizen = _userService.GetCitizenById(id);
        if (citizen == null) return NotFound();
        return Ok(citizen);
    }

    [HttpGet("citizens")]
    public ActionResult<IEnumerable<Citizen>> GetAllCitizens()
    {
        return Ok(_userService.GetAllCitizens());
    }

    // Companies
    [HttpPost("companies")]
    public ActionResult<Company> RegisterCompany([FromBody] RegisterCompanyRequest request)
    {
        try
        {
            var company = _userService.RegisterCompany(request.Name, request.Email, request.CompanyName, request.RegistrationNumber, request.Industry);
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("companies/{id}")]
    public ActionResult<Company> GetCompany(string id)
    {
        var company = _userService.GetCompanyById(id);
        if (company == null) return NotFound();
        return Ok(company);
    }

    [HttpGet("companies")]
    public ActionResult<IEnumerable<Company>> GetAllCompanies()
    {
        return Ok(_userService.GetAllCompanies());
    }

    // Government
    [HttpPost("governments")]
    public ActionResult<Government> RegisterGovernment([FromBody] RegisterGovernmentRequest request)
    {
        try
        {
            var government = _userService.RegisterGovernment(request.Name, request.Email, request.AgencyName, request.Department);
            return CreatedAtAction(nameof(GetGovernment), new { id = government.Id }, government);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("governments/{id}")]
    public ActionResult<Government> GetGovernment(string id)
    {
        var government = _userService.GetGovernmentById(id);
        if (government == null) return NotFound();
        return Ok(government);
    }

    [HttpGet("governments")]
    public ActionResult<IEnumerable<Government>> GetAllGovernments()
    {
        return Ok(_userService.GetAllGovernments());
    }
}
