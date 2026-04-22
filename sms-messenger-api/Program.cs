using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using sms_messenger_api.Models;
using sms_messenger_api.Services;

var builder = WebApplication.CreateBuilder(args);

var appConfigConnectionString = Environment.GetEnvironmentVariable("AZURE_APP_CONFIG_CONNECTION_STRING")
    ?? builder.Configuration["AZURE_APP_CONFIG_CONNECTION_STRING"];

if (!string.IsNullOrWhiteSpace(appConfigConnectionString))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(appConfigConnectionString);
        options.Select(KeyFilter.Any, LabelFilter.Null)
               .Select(KeyFilter.Any, builder.Environment.EnvironmentName);
        options.ConfigureRefresh(refresh =>
            refresh.Register(key: "App:Settings:Sentinel", refreshAll: true)
                   .SetRefreshInterval(TimeSpan.FromMinutes(5)));
    });
    builder.Services.AddAzureAppConfiguration();
}

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<TwilioSettings>(
    builder.Configuration.GetSection(TwilioSettings.SectionName));

builder.Services.AddScoped<ISmsService, SmsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

if (!string.IsNullOrWhiteSpace(appConfigConnectionString))
{
    app.UseAzureAppConfiguration();
}

app.UseCors("ReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
