using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookClient
{
    //TODO: Implement facebook client
    public Task<string> PublishPostAsync(
        string pageId,
        string accessToken,
        CalendarPost post,
        CancellationToken ct)
    {
        return Task.FromResult("123123123");
    }
}