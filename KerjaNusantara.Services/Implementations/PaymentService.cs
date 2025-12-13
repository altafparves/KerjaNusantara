using KerjaNusantara.Domain.Models.Employment;
using KerjaNusantara.Domain.Enums;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Interfaces;

namespace KerjaNusantara.Services.Implementations;

/// <summary>
/// Payment service implementation
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepo;
    private readonly IUserRepository<Domain.Models.Users.Citizen> _citizenRepo;
    private readonly IJobRepository _jobRepo;

    public PaymentService(
        IPaymentRepository paymentRepo,
        IUserRepository<Domain.Models.Users.Citizen> citizenRepo,
        IJobRepository jobRepo)
    {
        _paymentRepo = paymentRepo;
        _citizenRepo = citizenRepo;
        _jobRepo = jobRepo;
    }

    public Payment ProcessPayment(string citizenId, string jobId, decimal amount)
    {
        var citizen = _citizenRepo.GetById(citizenId);
        if (citizen == null)
            throw new InvalidOperationException("Citizen not found");

        var job = _jobRepo.GetById(jobId);
        if (job == null)
            throw new InvalidOperationException("Job not found");

        // Check if payment already exists for this job
        var existingPayment = _paymentRepo.GetByJobId(jobId);
        if (existingPayment != null)
            throw new InvalidOperationException("Payment already processed for this job");

        var payment = new Payment
        {
            Id = Guid.NewGuid().ToString(),
            CitizenId = citizenId,
            JobId = jobId,
            JobTitle = job.Title,
            Amount = amount,
            Status = PaymentStatus.Pending,
            CreatedDate = DateTime.Now,
            PaymentMethod = "Bank Transfer"
        };

        _paymentRepo.Add(payment);
        return payment;
    }

    public Payment? GetPaymentById(string id) => _paymentRepo.GetById(id);

    public IEnumerable<Payment> GetPaymentsByCitizen(string citizenId) => _paymentRepo.GetByCitizenId(citizenId);

    public decimal GetTotalEarnings(string citizenId) => _paymentRepo.GetTotalEarnings(citizenId);

    public void CompletePayment(string paymentId)
    {
        var payment = _paymentRepo.GetById(paymentId);
        if (payment == null)
            throw new InvalidOperationException("Payment not found");

        payment.Status = PaymentStatus.Completed;
        payment.CompletedDate = DateTime.Now;
        _paymentRepo.Update(payment);

        // Update citizen balance
        var citizen = _citizenRepo.GetById(payment.CitizenId);
        if (citizen != null)
        {
            citizen.Balance += payment.Amount;
            _citizenRepo.Update(citizen);
        }
    }
}
