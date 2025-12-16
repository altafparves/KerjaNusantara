namespace KerjaNusantara.API.DTOs;

public record ProcessPaymentRequest(string CitizenId, string JobId, decimal Amount);
