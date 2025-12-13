using Microsoft.Extensions.DependencyInjection;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Repository.Implementations;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Services.Implementations;
using KerjaNusantara.Services.Factories;
using KerjaNusantara.Services.Matching;
using KerjaNusantara.Domain.Models.Users;

namespace KerjaNusantara.ConsoleApp.Configuration;

/// <summary>
/// Dependency Injection configuration
/// </summary>
public static class ServiceConfiguration
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register Repositories (Singleton - one instance throughout app lifetime)
        services.AddSingleton<IUserRepository<Citizen>, CitizenRepository>();
        services.AddSingleton<IUserRepository<Company>, CompanyRepository>();
        services.AddSingleton<IUserRepository<Government>, GovernmentRepository>();
        services.AddSingleton<IJobRepository, JobRepository>();
        services.AddSingleton<IApplicationRepository, ApplicationRepository>();
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        services.AddSingleton<ITenderBidRepository, TenderBidRepository>();
        services.AddSingleton<IPaymentRepository, PaymentRepository>();

        // Register Factories (Singleton)
        services.AddSingleton<IUserFactory, UserFactory>();

        // Register Strategies (Transient - new instance each time)
        services.AddTransient<IMatchingStrategy, SkillBasedMatcher>();

        // Register Services (Transient)
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IMatchingService, MatchingService>();
        services.AddTransient<IJobService, JobService>();
        services.AddTransient<ITenderService, TenderService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IAnalyticsService, AnalyticsService>();

        return services.BuildServiceProvider();
    }
}
