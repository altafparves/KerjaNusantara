using KerjaNusantara.Domain.Models.Employment;

namespace KerjaNusantara.Services.Interfaces;

/// <summary>
/// Service interface for Payment processing
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Process payment for a completed job
    /// </summary>
    Payment ProcessPayment(string citizenId, string jobId, decimal amount);
    
    /// <summary>
    /// Get payment by ID
    /// </summary>
    Payment? GetPaymentById(string id);
    
    /// <summary>
    /// Get all payments for a citizen
    /// </summary>
    IEnumerable<Payment> GetPaymentsByCitizen(string citizenId);
    
    /// <summary>
    /// Get total earnings for a citizen
    /// </summary>
    decimal GetTotalEarnings(string citizenId);
    
    /// <summary>
    /// Complete a payment
    /// </summary>
    void CompletePayment(string paymentId);
}
