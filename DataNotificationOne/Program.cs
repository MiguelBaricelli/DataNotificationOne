using DataNotificationOne.Application;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Application.Services;
using DataNotificationOne.Application.Services.Email;
using DataNotificationOne.Infrastructure.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddUserSecrets<Program>();

var getApiKey = builder.Configuration["ApiKeys:AlphaVantage"];

if (string.IsNullOrEmpty(getApiKey))
{
    Console.WriteLine("Chave da api Alpha Vantage não encontrada no User Secrets");
    throw new ArgumentException("Chave da api Alpha Vantage não encontrada no User Secrets");
}

builder.Services.AddScoped<IFinanceSummaryVarianceService,FinanceSummaryVarianceService>();
builder.Services.AddScoped<IWeeklyDataForConsultService,WeeklyDataForConsultService>();
builder.Services.AddScoped<IDataOverviewService, DataOverviewService>();
builder.Services.AddScoped<IDailyConsultService, DailyConsultService>();
builder.Services.AddScoped<IGeneralResponseService, GeneralResponseService>();
builder.Services.AddScoped<IGenerateMessageDailyService,GenerateMessageDailyService>();
builder.Services.AddScoped<IEmailExecutor, EmailExecutor>();



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
