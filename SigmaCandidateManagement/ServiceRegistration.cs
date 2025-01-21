using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SigmaCandidateManagement.Business.Services;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Core.Interfaces.Services;
using SigmaCandidateManagement.Data.Repositories;
using System.Text;

namespace SigmaCandidateManagement
{
    public static class ServiceRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            // All repositories will be registered here in order to make program.cs simple
            services.AddScoped<ICandidateRepo, CandidateRepo>();

        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            // All services will be registered here in order to make program.cs simple
            services.AddScoped<ICandidateService, CandidateService>();

        }
        public static void AddOtherServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
            });
            //Configuring MVC Controllers
            services.AddControllers();


        }

    }
}
