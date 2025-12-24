using DataNotificationOne.Application;
using DataNotificationOne.Application.Services;
using DataNotificationOne.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddUserSecrets<Program>();


var apiKey = builder.Configuration["ApiKeys:KeyApiFinance"];

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Chave da api não encontrada no User Secrets");
    throw new ArgumentException("Chave da api não encontrada no User Secrets");
}

builder.Services.AddScoped<GetFinanceSummaryVarianceService>();
builder.Services.AddScoped<GetWeeklyDataForConsultService>();


builder.Services.AddHttpClient();

builder.Services.AddDependencyInjection();
// Add services to the container.

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
