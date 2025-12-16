namespace KerjaNusantara.API.DTOs;

public record RegisterCitizenRequest(string Name, string Email, string NIK, string Location);

public record RegisterCompanyRequest(string Name, string Email, string CompanyName, string RegistrationNumber, string Industry);

public record RegisterGovernmentRequest(string Name, string Email, string AgencyName, string Department);

public record UpdateCitizenRequest(string Id, string Name, string Email, string NIK, string Location, int YearsOfExperience);
