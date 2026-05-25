using SocialPublisherWorker;
using SocialPublisherWorker.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WeeklyPostWorker>();

var host = builder.Build();
host.Run();