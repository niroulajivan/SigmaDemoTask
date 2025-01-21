using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SigmaCandidateManagement;
using SigmaCandidateManagement.Business.Services;
using SigmaCandidateManagement.Core.Entities.Configuration;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Core.Interfaces.Services;
using SigmaCandidateManagement.Data;
using SigmaCandidateManagement.Data.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Configur Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CandidateMgmt")));

// Configuring Authentication and JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

// Configuring Application-Specific Settings
builder.Services.Configure<LoginParam>(builder.Configuration.GetSection("AdminCredentials"));

// Register Services and Repositories
builder.Services.AddScoped<ITokenService, TokenService>(); // Token Service
builder.Services.AddRepositoryServices(); // Custom extension to register all repositories
builder.Services.AddBusinessServices();   // Custom extension to register all business services

builder.Services.AddOtherServices();

// Build the application
var app = builder.Build();

// Ensure that the database is created on application start (if not already created)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        var invariantSetting = Environment.GetEnvironmentVariable("DOTNET_SYSTEM_GLOBALIZATION_INVARIANT");
        Console.WriteLine($"DOTNET_SYSTEM_GLOBALIZATION_INVARIANT: {invariantSetting ?? "Not Set"}");

        dbContext.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        // Handle potential exceptions (e.g., connection issues)
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
// Configuring the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllers();

app.Run();
