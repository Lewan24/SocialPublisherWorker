using System.Net;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using SocialPublisherWorker.Worker;
using Serilog;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Application.Services;
using SocialPublisherWorker.Infrastructure.Persistence;
using SocialPublisherWorker.Infrastructure.Persistence.Repositories;
using SocialPublisherWorker.Infrastructure.Social.Facebook;
using SocialPublisherWorker.Infrastructure.Time;

var builder = Host.CreateApplicationBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.AddSerilog(logger, dispose: true);

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(
    ILogger<DefaultHttpClient> logger)
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .Or<HttpRequestException>()
        .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt =>
                TimeSpan.FromSeconds(
                    Math.Pow(2, retryAttempt))
                + TimeSpan.FromMilliseconds(
                    Random.Shared.Next(0, 500)),
            onRetry: (outcome, timespan, retryAttempt, _) =>
            {
                logger.LogWarning(
                    "HTTP retry {RetryAttempt} after {Delay}s",
                    retryAttempt,
                    timespan.TotalSeconds);
            });
}

static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
{
    return Policy.TimeoutAsync<HttpResponseMessage>(
        TimeSpan.FromSeconds(30));
}

builder.Services
    .AddHttpClient<IDefaultHttpClient, DefaultHttpClient>(client =>
    {
        client.Timeout = Timeout.InfiniteTimeSpan;
    })
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler((sp, request) =>
    {
        var loggerService = sp.GetRequiredService<
            ILogger<DefaultHttpClient>>();
        
        return request.Method == HttpMethod.Get
            ? GetRetryPolicy(loggerService)
            : Policy.NoOpAsync<HttpResponseMessage>();
    })
    .AddPolicyHandler(GetTimeoutPolicy());

builder.Services.AddTransient<IClock, SystemClock>();

builder.Services.AddScoped<IPostScheduler, PostScheduler>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<ICalendarGenerator, CalendarGenerator>();

builder.Services.AddScoped<FacebookPublisher>();
builder.Services.AddScoped<FacebookClient>();
builder.Configuration.AddEnvironmentVariables();
builder.Services
    .AddOptions<FacebookOptions>()
    .BindConfiguration("Facebook")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHostedService<WeeklyPostWorker>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=SocialPublisher.db");
});
builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();

var host = builder.Build();

using var scope = host.Services.CreateScope();
var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
appDbContext.Database.Migrate();

await host.RunAsync();