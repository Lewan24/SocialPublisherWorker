namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookOptions
{
    public const string SectionName = "Facebook";

    public required string PageId { get; init; }
}