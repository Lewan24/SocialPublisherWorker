namespace SocialPublisherWorker.Domain.Entities;

public sealed class SchedulerOptions
{
    public const string SectionName = "Scheduler";

    public required string Cron { get; init; }

    public required string Timezone { get; init; }
}