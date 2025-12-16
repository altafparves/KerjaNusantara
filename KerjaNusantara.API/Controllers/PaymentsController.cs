using Microsoft.AspNetCore.Mvc;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.API.DTOs;

namespace KerjaNusantara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public ActionResult<Payment> ProcessPayment([FromBody] ProcessPaymentRequest request)
    {
        try
        {
            var payment = _paymentService.ProcessPayment(request.CitizenId, request.JobId, request.Amount);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Payment> GetPayment(string id)
    {
        var payment = _paymentService.GetPaymentById(id);
        if (payment == null) return NotFound();
        return Ok(payment);
    }

    [HttpGet("citizen/{citizenId}")]
    public ActionResult<IEnumerable<Payment>> GetPaymentsByCitizen(string citizenId)
    {
        return Ok(_paymentService.GetPaymentsByCitizen(citizenId));
    }

    [HttpGet("citizen/{citizenId}/earnings")]
    public ActionResult<decimal> GetTotalEarnings(string citizenId)
    {
        return Ok(_paymentService.GetTotalEarnings(citizenId));
    }

    [HttpPost("{id}/complete")]
    public ActionResult CompletePayment(string id)
    {
        _paymentService.CompletePayment(id);
        return Ok();
    }
}
