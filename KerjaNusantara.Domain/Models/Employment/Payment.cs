using KerjaNusantara.Domain.Interfaces;
using KerjaNusantara.Domain.Enums;

namespace KerjaNusantara.Domain.Models.Employment;

public class Payment : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CitizenId { get; set; } = string.Empty;
    public string JobId { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? CompletedDate { get; set; }
    public string PaymentMethod { get; set; } = "Bank Transfer";
}
