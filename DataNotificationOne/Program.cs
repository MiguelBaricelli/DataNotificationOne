using DataNotificationOne.Application.Services.Daily;
using DataNotificationOne.Application.Services.EmailMessage;
using DataNotificationOne.Application.Services.General;
using DataNotificationOne.Application.Services.Overview;
using DataNotificationOne.Application.Services.Weekly;
using DataNotificationOne.Infrastructure.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddUserSecrets<Program>();

/*
var apiKey = builder.Configuration["ApiKeys:KeyApiFinance"];

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Chave da api não encontrada no User Secrets");
    throw new ArgumentException("Chave da api não encontrada no User Secrets");
}
*/
var getApiKey = builder.Configuration["ApiKeys:AlphaVantage"];

if (string.IsNullOrEmpty(getApiKey))
{
    Console.WriteLine("Chave da api Alpha Vantage não encontrada no User Secrets");
    throw new ArgumentException("Chave da api Alpha Vantage não encontrada no User Secrets");
}

builder.Services.AddScoped<FinanceSummaryVarianceService>();
builder.Services.AddScoped<WeeklyDataForConsultService>();
builder.Services.AddScoped<DataOverviewService>();
builder.Services.AddScoped<GeneralResponseService>();
builder.Services.AddScoped<GenerateMessageDailyService>();




builder.Services.AddHttpClient();

builder.Services.AddDependencyInjection();
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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
