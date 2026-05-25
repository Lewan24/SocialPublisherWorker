using SocialPublisherWorker.Worker;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Logging.AddSerilog(logger, dispose: true);
builder.Services.AddHostedService<WeeklyPostWorker>();

var host = builder.Build();
await host.RunAsync();