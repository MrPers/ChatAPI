using ChatAPI.Data;
using ChatAPI.Hubs;
using ChatAPI.Interfaces;
using ChatAPI.Models;
using ChatAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Integrate Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
    options.Connect(builder.Configuration["ConnectionStrings:AzureAppConfig"]));

// Configure SignalR with Azure SignalR
var signalRConnection = builder.Configuration.GetConnectionString("SignalConnection");
builder.Services.AddSignalR().AddAzureSignalR(signalRConnection);

// Register settings for external services
builder.Services.Configure<AzureTextAnalyticsSettings>(
    builder.Configuration.GetSection("AzureTextAnalytics"));

// Add Entity Framework DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatDatabase")));

// Register application services
builder.Services.AddScoped<IChatConnectionService, ChatConnectionService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<ITextAnalyticsService, TextAnalyticsService>();

// Configure CORS to allow client-server communication
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("chatfrontend-f4e5cqc7hxc0gxep.uksouth-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Apply middleware
app.UseCors();

// Map SignalR hubs
app.MapHub<ChatHub>("/chat");

// Start the application
app.Run();
