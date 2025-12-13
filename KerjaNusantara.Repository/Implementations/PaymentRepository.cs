using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;

namespace KerjaNusantara.Repository.Implementations;

/// <summary>
/// Repository for Payment entities
/// </summary>
public class PaymentRepository : JsonRepository<Payment>, IPaymentRepository
{
    public PaymentRepository() : base("payments.json")
    {
    }

    public IEnumerable<Payment> GetByCitizenId(string citizenId)
    {
        return Find(p => p.CitizenId == citizenId);
    }

    public IEnumerable<Payment> GetByStatus(PaymentStatus status)
    {
        return Find(p => p.Status == status);
    }

    public Payment? GetByJobId(string jobId)
    {
        return _data.FirstOrDefault(p => p.JobId == jobId);
    }

    public decimal GetTotalEarnings(string citizenId)
    {
        return _data
            .Where(p => p.CitizenId == citizenId && p.Status == PaymentStatus.Completed)
            .Sum(p => p.Amount);
    }
}
