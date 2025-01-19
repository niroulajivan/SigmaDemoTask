using SigmaCandidateManagement.Business.Services;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Core.Interfaces.Services;
using SigmaCandidateManagement.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICandidateRepo, CandidateRepo>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
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
