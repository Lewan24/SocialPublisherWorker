using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Domain.Entities;

public sealed class PostPublication
{
    public Guid Id { get; set; }

    public required SocialPlatform Platform { get; init; }

    public required DateOnly WeekStart { get; init; }

    public DateTimeOffset PublishedAtUtc { get; init; }

    public required string ExternalPostId { get; init; }
}