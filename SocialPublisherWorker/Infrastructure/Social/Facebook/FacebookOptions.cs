namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookOptions
{
    public const string SectionName = "Facebook";

    /// <summary>
    /// ENV Facebook__PageId
    /// </summary>
    public string PageId { get; init; } = string.Empty;
    /// <summary>
    /// ENV Facebook__AccessToken
    /// </summary>
    public string AccessToken { get; init; }  = string.Empty;
}