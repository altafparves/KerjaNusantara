using KerjaNusantara.Domain.Models.Users;
using KerjaNusantara.Repository.Implementations;
using KerjaNusantara.Repository.Interfaces;
using KerjaNusantara.Services.Factories;
using KerjaNusantara.Services.Implementations;
using KerjaNusantara.Services.Interfaces;
using KerjaNusantara.Services.Matching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Repositories (Singleton - one instance throughout app lifetime)
builder.Services.AddSingleton<IUserRepository<Citizen>, CitizenRepository>();
builder.Services.AddSingleton<IUserRepository<Company>, CompanyRepository>();
builder.Services.AddSingleton<IUserRepository<Government>, GovernmentRepository>();
builder.Services.AddSingleton<IJobRepository, JobRepository>();
builder.Services.AddSingleton<IApplicationRepository, ApplicationRepository>();
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
builder.Services.AddSingleton<ITenderBidRepository, TenderBidRepository>();
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();

// Register Factories (Singleton)
builder.Services.AddSingleton<IUserFactory, UserFactory>();

// Register Strategies (Transient - new instance each time)
builder.Services.AddTransient<IMatchingStrategy, SkillBasedMatcher>();

// Register Services (Transient)
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMatchingService, MatchingService>();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<ITenderService, TenderService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IAnalyticsService, AnalyticsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
