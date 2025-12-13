using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Repository.Interfaces;

/// <summary>
/// Repository interface for Payment entities
/// </summary>
public interface IPaymentRepository : IRepository<Payment>
{
    /// <summary>
    /// Get all payments for a specific citizen
    /// </summary>
    IEnumerable<Payment> GetByCitizenId(string citizenId);

    /// <summary>
    /// Get payments by status
    /// </summary>
    IEnumerable<Payment> GetByStatus(PaymentStatus status);

    /// <summary>
    /// Get payment for a specific job
    /// </summary>
    Payment? GetByJobId(string jobId);

    /// <summary>
    /// Get total earnings for a citizen
    /// </summary>
    decimal GetTotalEarnings(string citizenId);
}
