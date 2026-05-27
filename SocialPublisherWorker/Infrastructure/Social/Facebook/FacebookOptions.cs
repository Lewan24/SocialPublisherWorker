namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookOptions
{
    public const string SectionName = "Facebook";

    public required string PageId { get; init; }
    
    // TODO: Move below to env
    public required string AppId { get; init; }
    public required string AppSecret { get; init; }
    public required string AccessToken { get; init; }
}