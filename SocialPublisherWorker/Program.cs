using SocialPublisherWorker.Worker;
using Serilog;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Application.Services;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Infrastructure.Social.Facebook;
using SocialPublisherWorker.Infrastructure.Time;

var builder = Host.CreateApplicationBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Logging.AddSerilog(logger, dispose: true);
builder.Services.AddHttpClient();

builder.Services.AddSingleton<SchedulerOptions>();

builder.Services.AddTransient<IClock, SystemClock>();

builder.Services.AddScoped<IPostScheduler, PostScheduler>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<ICalendarGenerator, CalendarGenerator>();

builder.Services.AddScoped<FacebookPublisher>();
builder.Configuration.AddEnvironmentVariables();
builder.Services
    .AddOptions<FacebookOptions>()
    .BindConfiguration("Facebook")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHostedService<WeeklyPostWorker>();

var host = builder.Build();
await host.RunAsync();