using SocialPublisherWorker.Worker;
using Serilog;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Application.Services;
using SocialPublisherWorker.Infrastructure.Social.Facebook;

var builder = Host.CreateApplicationBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Logging.AddSerilog(logger, dispose: true);

builder.Services.AddScoped<IPostScheduler, PostScheduler>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<ICalendarGenerator, CalendarGenerator>();
builder.Services.AddScoped<FacebookPublisher>();

builder.Services.AddHostedService<WeeklyPostWorker>();

var host = builder.Build();
await host.RunAsync();